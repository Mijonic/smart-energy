using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.Enums;
using Microsoft.EntityFrameworkCore;
using SmartEnergyDomainModels;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.User;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using SmartEnergy.Contract.CustomExceptions.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace SmartEnergy.Service.Services
{
    public class UserService : IUserService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IAuthHelperService _authHelperService;

        public UserService(SmartEnergyDbContext dbContext, IConfiguration configuration, IMailService mailService, IMapper mapper, IAuthHelperService authHelperService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
            _authHelperService = authHelperService;
        }

        public UserDto ApproveUser(int userId)
        {
            User user = _dbContext.Users.Find(userId);
            if (user == null)
                throw new UserNotFoundException($"User does not exist.");
            if(user.UserStatus != UserStatus.PENDING)
                throw new UserInvalidStatusException("User can't be approved , as his status is not Pending.");

            user.UserStatus = UserStatus.APPROVED;
            _mailService.SendMail(user.Email, "Registration status", "Your registration to our site has been approved.");
            _dbContext.SaveChanges();

            
            return _mapper.Map<UserDto>(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto DenyUser(int userId)
        {
            User user = _dbContext.Users.Find(userId);
            if (user == null)
                throw new UserNotFoundException($"User does not exist.");
            if (user.UserStatus != UserStatus.PENDING)
                throw new UserInvalidStatusException("User can't be denied , as his status is not Pending.");

            user.UserStatus = UserStatus.DENIED;
            user.CrewID = null;
            

            _mailService.SendMail(user.Email, "Registration status", "Your registration to our site has been denied.");
            _dbContext.SaveChanges();
            return _mapper.Map<UserDto>(user);
        }

        public UserDto Get(int id)
        {
            return _mapper.Map<UserDto>(_dbContext.Users.Find(id));
        }

        public List<UserDto> GetAll()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Include(x => x.Location).ToList());
        }

        public List<UserDto> GetAllUnassignedCrewMembers()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Where(x => x.UserType == UserType.CREW_MEMBER && x.CrewID == null && x.UserStatus == UserStatus.APPROVED).ToList());
        }

        public UsersListDto GetUsersPaged(UserField sortBy, SortingDirection direction, int page, int perPage, UserStatusFilter status, UserTypeFilter type, string searchParam)
        {
            IQueryable<User> usersPaged = _dbContext.Users.Include(x => x.Location).AsQueryable();

            usersPaged = FilterUsersByStatus(usersPaged, status);
            usersPaged = FilterUsersByType(usersPaged, type);
            usersPaged = SearchUsers(usersPaged, searchParam);
            usersPaged = SortUsers(usersPaged, sortBy, direction);
            
            int resourceCount = usersPaged.Count();
            usersPaged = usersPaged.Skip(page * perPage)
                                    .Take(perPage);

            UsersListDto returnValue = new UsersListDto()
            {
                Users = _mapper.Map<List<UserDto>>(usersPaged.ToList()),
                TotalCount = resourceCount
            };

            return returnValue;

        }

        public UserDto Insert(UserDto entity)
        {
            Location userLocation = _dbContext.Location.Find(entity.Location.ID);
            if(userLocation == null)
            { 
                throw new Exception();
            }

            User user = _mapper.Map<User>(entity);


            if (_dbContext.Users.FirstOrDefault(x => x.Email == user.Email) != null)
                throw new InvalidUserDataException($"User with email address {user.Email} already exists.");

            if (user.UserType == UserType.ADMIN)
                throw new InvalidUserDataException("User cannot register as admin!");

            if(user.UserType != UserType.CREW_MEMBER && user.Crew != null)
                throw new InvalidUserDataException("User can be part of a crew only if he is a crew member!");

            if(user.Crew != null)
            {
                Crew crew = _dbContext.Crews.Find(user.Crew.ID);
                if (crew == null)
                    throw new CrewNotFoundException("Selected user crew does not exist.");

            }

            user.ID = 0;
            user.UserStatus = UserStatus.PENDING;//Just in case
            user.LocationID = userLocation.ID;
            user.Location = null;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _dbContext.Users.Add(user);
            

            _mailService.SendMail(user.Email, "Registration status", "Your registration to our site is pending, when admins approve you you will have full access.");
            _dbContext.SaveChanges();
            return _mapper.Map<UserDto>(user).StripConfidentialData();

        }

        public string Login(LoginDto userInfo, out UserDto userData)
        {
            //TODO: Add password hashing
            User user = _dbContext.Users.Where(x => x.Username == userInfo.Username).FirstOrDefault();

            if (user == null)
                throw new UserNotFoundException($"User with username {userInfo.Username} does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(userInfo.Password, user.Password) )
                throw new InvalidUserDataException($"Incorrect password.");

            if(user.UserStatus == UserStatus.DENIED)
                throw new UserInvalidStatusException($"User is blocked by admin.");

            string tokenString = _authHelperService.CreateToken(_mapper.Map<UserDto>(user));
            userData = _mapper.Map<UserDto>(user).StripConfidentialData();
            return tokenString;
        }


        public async Task<LoginResponseDto> LoginExternal(ExternalLoginDto userInfo)
        {
            SocialInfoDto socialInfo;
            if(userInfo.Provider == "FACEBOOK")
            {
                socialInfo = await _authHelperService.VerifyFacebookTokenAsync(userInfo);
            }
            else
            {
                socialInfo = await _authHelperService.VerifyGoogleToken(userInfo);
            }

            if (socialInfo == null)
                throw new InvalidTokenException("Token invalid");
            User userInDb = _dbContext.Users.FirstOrDefault(x => x.Email == socialInfo.Email);
            if(userInDb == null)
            {
                userInDb = new User()
                {
                    Email = socialInfo.Email,
                    Username = socialInfo.Email.Substring(0,socialInfo.Email.IndexOf("@")),
                    Name = socialInfo.Name,
                    Lastname = socialInfo.LastName,
                    Password = socialInfo.ID,
                    BirthDay = DateTime.Now, //Because user might have made this data private on account
                    UserType = UserType.CONSUMER,
                    LocationID = 1, //Some default location untill specified
                    UserStatus = UserStatus.PENDING
                };
                _dbContext.Users.Add(userInDb);
                _dbContext.SaveChanges();

            }

            string token = _authHelperService.CreateToken(_mapper.Map<UserDto>(userInDb));
            return new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(userInDb).StripConfidentialData(),
                Token = token,
                IsSuccessfull = true

            };

        }

        public UserDto Update(UserDto entity)
        {
            throw new NotImplementedException();
        }

        private IQueryable<User> FilterUsersByStatus(IQueryable<User> users, UserStatusFilter status)
        {
            //Filter by status, ignore if ALL
            switch (status)
            {
                case UserStatusFilter.approved:
                    return users.Where(x => x.UserStatus == UserStatus.APPROVED);
                case UserStatusFilter.denied:
                    return users.Where(x => x.UserStatus == UserStatus.DENIED);
                case UserStatusFilter.pending:
                    return users.Where(x => x.UserStatus == UserStatus.PENDING);
            }

            return users;
        }


        private IQueryable<User> FilterUsersByType(IQueryable<User> users, UserTypeFilter type)
        {
            //Filter by TYPE, ignore if ALL
            switch (type)
            {
                case UserTypeFilter.admin:
                    return users.Where(x => x.UserType == UserType.ADMIN);
                case UserTypeFilter.consumer:
                    return users.Where(x => x.UserType == UserType.CONSUMER);
                case UserTypeFilter.crew_member:
                    return users.Where(x => x.UserType == UserType.CREW_MEMBER);
                case UserTypeFilter.dispatcher:
                    return users.Where(x => x.UserType == UserType.DISPATCHER);
                case UserTypeFilter.worker:
                    return users.Where(x => x.UserType == UserType.WORKER);
            }

            return users;
        }


        private IQueryable<User> SearchUsers(IQueryable<User> users, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam)) //Ignore empty search
                return users;
            ///Perform search
            return users.Where(x => x.Email.Contains(searchParam) ||
                                               x.Name.Contains(searchParam) ||
                                               x.Lastname.Contains(searchParam) ||
                                               x.Username.Contains(searchParam) ||
                                               x.BirthDay.ToString().Contains(searchParam) ||
                                               x.Location.Street.Contains(searchParam) ||
                                               x.Location.City.Contains(searchParam) ||
                                               x.Location.Zip.Contains(searchParam));
        }


        private IQueryable<User> SortUsers(IQueryable<User> users, UserField sortBy, SortingDirection direction)
        {
            //Sort
            if (direction == SortingDirection.asc)
            {
                switch (sortBy)
                {
                    case UserField.id:
                        return users.OrderBy(x => x.ID);
                    case UserField.birthDay:
                        return users.OrderBy(x => x.BirthDay);
                    case UserField.email:
                        return users.OrderBy(x => x.Email);
                    case UserField.lastname:
                        return users.OrderBy(x => x.Lastname);
                    case UserField.name:
                        return users.OrderBy(x => x.Name);
                    case UserField.username:
                        return users.OrderBy(x => x.Username);
                }

            }
            else
            {
                switch (sortBy)
                {
                    case UserField.id:
                        return users.OrderByDescending(x => x.ID);
                    case UserField.birthDay:
                        return users.OrderByDescending(x => x.BirthDay);
                    case UserField.email:
                        return users.OrderByDescending(x => x.Email);
                    case UserField.lastname:
                        return users.OrderByDescending(x => x.Lastname);
                    case UserField.name:
                        return users.OrderByDescending(x => x.Name);
                    case UserField.username:
                        return users.OrderByDescending(x => x.Username);
                }

            }

            return users;
        }
    }


}

using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergyDomainModels;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.Enums;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions.Crew;

namespace SmartEnergy.Service.Services
{
    public class CrewService : ICrewService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public CrewService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            Crew crew = _dbContext.Crews
                        .Include("CrewMembers")
                        .Include("Incidents")
                        .FirstOrDefault(x => x.ID == id);
            if (crew == null)
                throw new CrewNotFoundException("Crew not found");

            _dbContext.Crews.Remove(crew);
            _dbContext.SaveChanges();
        }

        public CrewDto Get(int id)
        {
            return _mapper.Map<CrewDto>(_dbContext.Crews.Include("CrewMembers").FirstOrDefault( x=> x.ID == id));
        }

        public List<CrewDto> GetAll()
        {
            return _mapper.Map<List<CrewDto>>(_dbContext.Crews.Include("CrewMembers").ToList());
        }

        public CrewsListDto GetCrewsPaged(CrewField sortBy, SortingDirection direction, int page, int pageSize)
        {
            IQueryable<Crew> pagedData = _dbContext.Crews
                                        .Include(x => x.CrewMembers)
                                        .AsQueryable();
            if (direction == SortingDirection.asc)
            {
                switch (sortBy)
                {
                    case CrewField.id:
                        pagedData = pagedData.OrderBy(x => x.ID);
                        break;
                    case CrewField.crewName:
                        pagedData = pagedData.OrderBy(x => x.CrewName);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case CrewField.id:
                        pagedData = pagedData.OrderByDescending(x => x.ID);
                        break;
                    case CrewField.crewName:
                        pagedData = pagedData.OrderByDescending(x => x.CrewName);
                        break;
                }
            }

            int resourceCount = pagedData.Count();
            pagedData = pagedData.Skip(page * pageSize)
                                 .Take(pageSize);

            CrewsListDto returnValue = new CrewsListDto()
            { 
                Crews = _mapper.Map<List<CrewDto>>(pagedData.ToList()),
                TotalCount = resourceCount
            };

            return returnValue;

        }

        public CrewDto Insert(CrewDto entity)
        {
            Crew newCrew = _mapper.Map<Crew>(entity);
                

            if(newCrew.CrewMembers != null)
            {
                if (newCrew.CrewName == null || newCrew.CrewName == "")
                    throw new InvalidCrewException("Crew has no name");
                if (newCrew.CrewMembers.Count == 0)
                    throw new InvalidCrewException("Crew must have at least 1 member");

                    List<User> crewMembers = new List<User>();
                foreach (User crewMember in newCrew.CrewMembers)//Validate each crew member
                {
                    User userInDb = _dbContext.Users.Find(crewMember.ID);
                    if (userInDb == null)
                        throw new UserNotFoundException($"User with id {userInDb.ID}, {crewMember.Name} {crewMember.Lastname} does not exist.");

                    if (userInDb.UserType != UserType.CREW_MEMBER)
                        throw new UserNotCrewMemberException($"User with id {userInDb.ID}, {crewMember.Name} {crewMember.Lastname} is not a CREW_MEMBER.");

                    if (userInDb.CrewID != null)
                        throw new UserAlreadyInCrewException($"User with id {userInDb.ID}, {crewMember.Name} {crewMember.Lastname} is already assigned to a crew");

                    if (newCrew == null)
                        throw new CrewNotFoundException("Crew not found");

                    //userInDb.CrewID = newCrew.ID;
                    crewMembers.Add(userInDb);

                }
                newCrew.CrewMembers = crewMembers;
            }
            newCrew.ID = 0;
            _dbContext.Crews.Add(newCrew);
            _dbContext.SaveChanges();

            return _mapper.Map<CrewDto>(newCrew);
        }

        public CrewDto Update(CrewDto entity)
        {
            Crew modifiedCrew = _mapper.Map<Crew>(entity);
            Crew oldCrew = _dbContext.Crews.Include("CrewMembers").FirstOrDefault(x => x.ID == entity.ID);

            if (oldCrew == null)
                throw new CrewNotFoundException($"Crew with id {entity.ID} not found");

            oldCrew.CrewMembers = new List<User>();

            if (modifiedCrew.CrewMembers != null)
            {
                foreach (User crewMember in modifiedCrew.CrewMembers)//Validate each crew member
                {
                    User userInDb = _dbContext.Users.Find(crewMember.ID);
                    if (userInDb == null)
                        throw new UserNotFoundException($"User with id {userInDb.ID} does not exist.");

                    if (userInDb.UserType != UserType.CREW_MEMBER)
                        throw new UserNotCrewMemberException($"User with id {userInDb.ID} is not a CREW_MEMBER.");

                    if (userInDb.CrewID != null && userInDb.CrewID != modifiedCrew.ID)
                        throw new UserAlreadyInCrewException($"User with id {userInDb.ID} is already assigned to a crew");

                    //crewMember.CrewID = newCrew.ID;
                    oldCrew.CrewMembers.Add(userInDb);
                }
            }

            oldCrew.UpdateCrew(modifiedCrew);
            _dbContext.SaveChanges();

            return _mapper.Map<CrewDto>(oldCrew);
        }
    }
}

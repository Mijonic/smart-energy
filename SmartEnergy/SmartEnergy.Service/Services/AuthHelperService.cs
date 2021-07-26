using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FacebookCore;
using FacebookCore.APIs;
using Newtonsoft.Json.Linq;

namespace SmartEnergy.Service.Services
{
    public class AuthHelperService : IAuthHelperService
    {
        private readonly IConfigurationSection _googleSettings;
        private readonly IConfigurationSection _facebookSettings;
        private readonly IConfigurationSection _secretKey;

        public AuthHelperService(IConfiguration config)
        {
            _googleSettings = config.GetSection("GoogleAuthSettings");
            _facebookSettings = config.GetSection("FacebookAuthSettings");
            _secretKey = config.GetSection("SecretKey");
        }

        public string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString())); //Add user type to claim
            claims.Add(new Claim(ClaimTypes.Email, user.Email)); //Add user email
            claims.Add(new Claim(ClaimTypes.Name, user.Name)); //Add name 
            claims.Add(new Claim(ClaimTypes.Surname, user.Lastname)); //Add lastname
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())); //Add ID
            if(user.UserStatus =="APPROVED")
                claims.Add(new Claim("Approved", "Approved"));

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44372",
                audience: "http://localhost:44372",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signinCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public int GetUserIDFromPrincipal(ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public async Task<SocialInfoDto> VerifyGoogleToken(ExternalLoginDto externalLogin)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleSettings.GetSection("clientId").Value }
                };
                var socialInfo = await GoogleJsonWebSignature.ValidateAsync(externalLogin.IdToken, settings);
                return new SocialInfoDto()
                {
                    ID = socialInfo.JwtId,
                    Name = socialInfo.GivenName,
                    LastName = socialInfo.FamilyName,
                    Email = socialInfo.Email,

                }; 
            }
            catch 
            {
                return null;
            }
        }

        public async Task<SocialInfoDto> VerifyFacebookTokenAsync(ExternalLoginDto externalLogin)
        {
            string[] userInfo = { "id", "name", "email", "first_name", "last_name" };
            if (string.IsNullOrEmpty(externalLogin.IdToken))
            {
                return null;
            }
            FacebookClient client = new FacebookClient(_facebookSettings.GetSection("clientId").Value,
                                                       _facebookSettings.GetSection("clientSecret").Value);
            try
            {
                FacebookUserApi api = client.GetUserApi(externalLogin.IdToken);
                JObject info = await api.RequestInformationAsync(userInfo);
                if (info == null)
                    return null;
                SocialInfoDto fbInfo = new SocialInfoDto()
                {
                    ID = (string)info["id"],
                    Name = (string)info["first_name"],
                    LastName = (string)info["last_name"],
                    Email = (string)info["email"],

                };
                return fbInfo; 
            }
            catch
            {
                return null;
            }
        }

    }
}

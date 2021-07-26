using Google.Apis.Auth;
using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IAuthHelperService
    {
        int GetUserIDFromPrincipal(ClaimsPrincipal user);
        Task<SocialInfoDto> VerifyGoogleToken(ExternalLoginDto externalLogin);
        Task<SocialInfoDto> VerifyFacebookTokenAsync(ExternalLoginDto externalLogin);
        string CreateToken(UserDto user);

    }
}

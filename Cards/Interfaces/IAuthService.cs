using Cards.DTOs;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Cards.Interfaces
{
    public interface IAuthService
    {
        public LoginResponse GenerateJwtToken(IdentityUser user, IdentityRole role);
        public string GetUserIdFromToken(ClaimsPrincipal currentUser);
        public string GetRoleFromToken(ClaimsPrincipal currentUser);


    }
}

using Cards.DTOs;
using Cards.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cards.Services
{
    public class AuthService : IAuthService
    {
        public LoginResponse GenerateJwtToken(IdentityUser user, IdentityRole role)
        {
            var userRoles = new List<string>();
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)

            };

            authClaims.Add(new Claim(ClaimTypes.Role, role.Name));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("logiceaCardsSecretKey#####22####"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                issuer: "logicea.cards.com",
                audience: "logicea.cards.com",
                signingCredentials: credentials
                );
            LoginResponse response = new LoginResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Role = role.Name,
                Roles = userRoles,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Tokenxpiry = token.ValidTo
            };
            return response;
        }
        public string GetUserIdFromToken(ClaimsPrincipal currentUser)
        {
            ClaimsIdentity identity = currentUser.Identity as ClaimsIdentity;
            string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
        public string GetRoleFromToken(ClaimsPrincipal currentUser)
        {
            ClaimsIdentity identity = currentUser.Identity as ClaimsIdentity;
            string role = identity.FindFirst(ClaimTypes.Role)?.Value;
            return role;
        }
    }
}

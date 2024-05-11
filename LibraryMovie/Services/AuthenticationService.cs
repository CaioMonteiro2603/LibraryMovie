using LibraryMovie.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryMovie.Services
{
    public class AuthenticationService
    {
        public static string GetToken(UsersModel usersModel)
        {
            byte[] secret = Encoding.ASCII.GetBytes(Settings.SECRET_TOKEN);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim( ClaimTypes.Name , usersModel.Name),
                    new Claim( ClaimTypes.Email, usersModel.Email),
                    new Claim( ClaimTypes.Role, usersModel.Role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "library",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}

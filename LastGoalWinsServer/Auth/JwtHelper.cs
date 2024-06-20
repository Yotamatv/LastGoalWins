using LastGoalWinsServer.Models.SQLModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LastGoalWinsServer.Auth
{
    public class JwtHelper
    {
        public static readonly string secretKey = "F8C2E94F-694F-4888-B434-7B0B228239D4";

        public static string GenerateAuthToken(UserSql user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim("id",user.Id.ToString()),
                new Claim("isAdmin",user.IsAdmin.ToString()),
                new Claim("first",user.FirstName.ToString()),
                new Claim ("balance",user.Balance.ToString()),
                new Claim ("type",user.IsAdmin ? "Admin":"User")
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "FootballServer",
                audience: "FootballFront",
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

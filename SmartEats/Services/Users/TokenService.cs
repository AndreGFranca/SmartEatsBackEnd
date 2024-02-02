using SmartEats.Models.Users;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SmartEats.Services.Users
{
    public class TokenService
    {
        private IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[] {
            new Claim("id",user.Id),
            new Claim("username",user.UserName),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())

        };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://api.smarteats", // Emissor do token
                audience: "http://api.smarteats", // Audiência do token (pode ser a mesma aplicação)
                notBefore: DateTime.UtcNow, // Token é válido a partir deste momento
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

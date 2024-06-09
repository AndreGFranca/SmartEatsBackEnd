using SmartEats.Models.Users;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SmartEats.Enums.Users;
using Microsoft.AspNetCore.Identity;

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
            new Claim("name",user.Name),
            new Claim("typeUser",((int)user.TypeUser).ToString()),       
            new Claim("loginTimestamp", DateTime.UtcNow.ToString()),
            new Claim("userName", user.UserName!),
            new Claim("cpf", user.CPF),
            new Claim("companyId", user.Id_Company.ToString()),
            new Claim(ClaimTypes.Role, user.TypeUser.ToString()),
        };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://api.smarteats", // Emissor do token
                audience: "http://api.smarteats", // Audiência do token (pode ser a mesma aplicação)
                notBefore: DateTime.UtcNow, // Token é válido a partir deste momento
                expires: DateTime.Now.AddMinutes(120),
                claims: claims,
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

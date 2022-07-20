using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bookshop.Entity;
using Microsoft.IdentityModel.Tokens;

namespace uitbooks.Token;

public interface IJwtUtils
    {
        public string GenerateToken(User user);
        // public String ValidateToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("svvafvaefvaefvarv"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
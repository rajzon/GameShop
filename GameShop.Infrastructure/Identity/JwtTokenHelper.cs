using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GameShop.Application.Helpers;
using GameShop.Domain.Model;
using GameShop.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GameShop.Infrastructure.Identity
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IOptions<JWTSettings> _jwtOptions;

        public JwtTokenHelper(IOptions<JWTSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public  async Task<string> GenerateJwtToken(User user, UserManager<User> userManager, IConfiguration config)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(_jwtOptions.Value.ExpireDays),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
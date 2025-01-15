using Database;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthService
    {
        private readonly TMSContext _db;
        private readonly IConfiguration _configuration;
        public AuthService(TMSContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<UserViewModel?> CheckLogin(LoginViewModel loginModel)
        {
            try
            {
                var query = from user in _db.User
                            join role in _db.Role on user.RoleId equals role.Id
                            where user.UserName == loginModel.Username && user.Password == loginModel.Password
                            select new UserViewModel
                            {
                                Id = user.Id,
                                Name = user.Name,
                                UserName = user.UserName,
                                Password = user.Password,
                                Role = new RoleViewModel
                                {
                                    Id = role.Id,
                                    Name = role.Name,
                                }
                            };
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GenerateJwtToken()
        {
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
                 _configuration["JWT:ValidIssuer"],
                 _configuration["JWT:ValidAudience"],
                 claimsIdentity.Claims,
                 expires: expires,
                 signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

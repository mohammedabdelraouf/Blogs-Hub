using BlogsAPI.Data;
using BlogsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogsAPI.Services
{
    public interface IAuthService
    {
        void Register(string username, string password, string role);
        string Login(string username, string password);
    }
    public class AuthService : IAuthService
    {
        private readonly BlogsDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(BlogsDbContext context , IConfiguration configuration)
        {
            _context = context; 
            _configuration = configuration;
        }
        public  void Register(string Email, string password, string role)
        {
            
            try
            {
                var user = new User();
                user.Name = "Mohamed";
                user.Email = Email;
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
                user.Role = role;
                _context.Users.Add(user);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Login(string Email , string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == Email);
            if (user != null && new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), 
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
        
            }
            throw new Exception("Invalid credentials !");
        }
    }
}

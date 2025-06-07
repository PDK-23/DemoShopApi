using DemoShopApi.Application.DTOs;
using DemoShopApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoShopApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto dto)
        {
            if (_db.Users.Any(u => u.Username == dto.Username))
                return BadRequest("Username already exists");

            // Lấy Role từ bảng Role
            var role = _db.Roles.SingleOrDefault(r => r.Id == dto.RoleId);
            if (role == null)
                return BadRequest("Role does not exist");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.Id
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok("Register success");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto dto)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _config["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }

    }
}

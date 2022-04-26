using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCbt_Backend.Data;
using WebCbt_Backend.Models;

namespace WebCbt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly WebCbtDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IConfiguration configuration, WebCbtDbContext context, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        // POST: /user
        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            if (await _userManager.FindByNameAsync(registerUser.Login) != null)
            {
                return Conflict();
            }

            var identityUser = new IdentityUser
            {
                UserName = registerUser.Login,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(identityUser, registerUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var user = new User
            {
                Id = identityUser.Id,
                Age = registerUser.Age,
                Gender = registerUser.Gender,
                UserStatus = 0,
                Banned = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: /user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // POST: /user/login
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            var identityUser = await _userManager.FindByNameAsync(loginUser.Login);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, loginUser.Password))
            {
                return Unauthorized();
            }

            return ProvideToken(identityUser);
        }

        // GET: /user/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private ActionResult ProvideToken(IdentityUser identityUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                issuer: _configuration["Jwt:Issuer"],
                signingCredentials: signingCredentials);

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}

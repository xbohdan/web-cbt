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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            if (await _userManager.FindByEmailAsync(registerUser.Login) != null)
            {
                return Conflict();
            }

            var index = registerUser.Login.IndexOf("@");

            if (index == -1)
            {
                return BadRequest();
            }

            var identityUser = new IdentityUser
            {
                UserName = registerUser.Login[..index],
                Email = registerUser.Login,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(identityUser, registerUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
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
            return Ok(await _context.Users.ToListAsync());
        }

        // POST: /user/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginUser.Login);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, loginUser.Password))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == identityUser.Id);

            if (user == null || user.IdNavigation.Email == null)
            {
                return StatusCode(500);
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.IdNavigation.Email)
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

        // GET: /user/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: /user/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(int userId, User user)
        {
            if (userId != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        private bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.UserId == userId);
        }

        // DELETE: /user/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

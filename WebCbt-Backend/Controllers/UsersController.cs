using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly WebCbtDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(IConfiguration configuration, WebCbtDbContext context, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
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
                UserId = identityUser.Id,
                Age = registerUser.Age,
                Gender = registerUser.Gender,
                UserStatus = 0,
                Banned = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            var identityUser = await _userManager.FindByNameAsync(loginUser.Login);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, loginUser.Password))
            {
                return Unauthorized();
            }

            return ProvideToken(identityUser);
        }

        private IActionResult ProvideToken(IdentityUser identityUser)
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

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}

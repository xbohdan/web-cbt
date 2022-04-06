using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCbt_Backend.Models;

namespace WebCbt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (await _userManager.FindByNameAsync(user.Login) != null)
            {
                return Conflict();
            }

            var identityUser = new IdentityUser
            {
                UserName = user.Login,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.Login);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, user.Password))
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

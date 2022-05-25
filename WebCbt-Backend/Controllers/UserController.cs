using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WebCbt_Backend.Data;
using WebCbt_Backend.Models;

namespace WebCbt_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("AllOrigins")]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly WebCbtDbContext _dbContext;

        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IConfiguration configuration, WebCbtDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // POST: /user
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            var index = registerUser.Login.IndexOf("@");

            if (index == -1 || registerUser.Password == null)
            {
                return BadRequest();
            }

            if (await _userManager.FindByEmailAsync(registerUser.Login) != null
                || await _userManager.FindByNameAsync(registerUser.Login[..index]) != null)
            {
                return Conflict();
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

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        // GET: /user
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized();
            }

            var userDtos = new List<UserDto>();
            foreach (var user in await _dbContext.Users.ToListAsync())
            {
                userDtos.Add(CreateUserDto(user));
            }
            return Ok(userDtos);
        }

        private static UserDto CreateUserDto(User user)
        {
            var userDto = new UserDto();

            var userDtoProps = typeof(UserDto).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanWrite);

            var userProps = typeof(User).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead);

            foreach (var userDtoProp in userDtoProps)
            {
                var userProp = userProps.FirstOrDefault(x => x.Name == userDtoProp.Name);

                if (userProp != null)
                {
                    var userValue = userProp.GetValue(user);

                    userDtoProp.SetValue(userDto, userValue);
                }
                else
                {
                    if (userDtoProp.Name == "Login")
                    {
                        userDtoProp.SetValue(userDto, user.IdNavigation.Email);
                    }
                }
            }

            return userDto;
        }

        // POST: /user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginUser.Login);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, loginUser.Password))
            {
                return Unauthorized();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == identityUser.Id);

            if (user == null)
            {
                return Problem();
            }

            var claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("userStatus", user.UserStatus.ToString())
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
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                user.UserStatus,
                user.UserId
            });
        }

        // GET: /user/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized();
            }

            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(CreateUserDto(user));
        }

        // PUT: /user/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(int userId, RegisterUser registerUser)
        {
            if (User.Identity?.IsAuthenticated != true || User.FindFirstValue("userId") != userId.ToString())
            {
                return Unauthorized();
            }

            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var aspNetUser = await _userManager.FindByIdAsync(user.Id);

            if (aspNetUser == null)
            {
                return Problem();
            }

            if (registerUser.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(aspNetUser);

                var result = await _userManager.ResetPasswordAsync(aspNetUser, token, registerUser.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.Select(x => x.Description));
                }
            }

            if (registerUser.Login != aspNetUser.Email)
            {
                var index = registerUser.Login.IndexOf("@");

                if (index == -1)
                {
                    return BadRequest();
                }

                if (await _userManager.FindByEmailAsync(registerUser.Login) != null
                    || await _userManager.FindByNameAsync(registerUser.Login[..index]) != null)
                {
                    return Conflict();
                }

                var result = await _userManager.SetUserNameAsync(aspNetUser, registerUser.Login[..index]);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.Select(x => x.Description));
                }

                var token = await _userManager.GenerateChangeEmailTokenAsync(aspNetUser, registerUser.Login);

                result = await _userManager.ChangeEmailAsync(aspNetUser, registerUser.Login, token);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.Select(x => x.Description));
                }
            }

            user.Age = registerUser.Age;

            user.Gender = registerUser.Gender;

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
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

            return Ok();
        }

        private bool UserExists(int userId)
        {
            return _dbContext.Users.Any(x => x.UserId == userId);
        }

        // DELETE: /user/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (User.Identity?.IsAuthenticated != true || User.FindFirstValue("userId") != userId.ToString())
            {
                return Unauthorized();
            }

            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}

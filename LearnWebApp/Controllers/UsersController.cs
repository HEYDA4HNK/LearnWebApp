using Core;
using LearnWebApp.Configs;
using LearnWebApp.RequestDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearnWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<AppUser> _userService;
        public UsersController(UserManager<AppUser> userService)
        {
            _userService = userService;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetByEmailUser(string userName)
        {
            var user = await _userService.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            var userToDTO = UserToDTO(user);
            return Ok(userToDTO);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest userDTO)
        {
            var user = DTOToUser(userDTO);

            try
            {
                var res = await _userService.CreateAsync(user, userDTO.Password);
                if (res.Succeeded)
                {
                    var token = CreateJWT(user);
                    var result = CreateLoginUser(user, token);
                    return Created($"/api/users/{user.Email}", result);
                }
                else
                {
                    return BadRequest(res.Errors);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserRequest userDTO)
        {
            var res = await _userService.FindByEmailAsync(userDTO.Email);
            if (res == null)
                return NotFound("Пользователь отсутствует");
            if (await _userService.CheckPasswordAsync(res, userDTO.Password))
            {
                var token = CreateJWT(res);
                var result = CreateLoginUser(res, token);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.Users.ToListAsync();
            if (users == null)
                return NotFound();
            var listUsers = new List<AppUserOutRequest>();
            foreach (var user in users)
                listUsers.Add(UserToDTO(user));
            return Ok(listUsers);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var nameOfUser = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.FindByNameAsync(nameOfUser);
            if (user == null)
                return NotFound();
            var profile = new ReturnUserProfileRequest()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.UserName
            };
            return Ok(profile);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteUser()
        {
            var nameOfUser = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.FindByNameAsync(nameOfUser);
            if (user == null)
            {
                return NotFound();
            }
            var res = await _userService.DeleteAsync(user);
            return Ok();
        }

        private string CreateJWT(AppUser user)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.UserName) };
            var jwtToken = new JwtSecurityToken
                (
                    issuer: JwtBearerConfig.Issuer,
                    audience: JwtBearerConfig.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(2),
                    signingCredentials: new SigningCredentials(JwtBearerConfig.GetKey(), SecurityAlgorithms.HmacSha256)
                );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        private ReturnLoginUserRequest CreateLoginUser(AppUser user, string token)
        {
            return new ReturnLoginUserRequest()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        private AppUserOutRequest UserToDTO(AppUser user)
        {
            return new AppUserOutRequest()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
            };
        }

        private AppUser DTOToUser(RegisterUserRequest user)
        {
            return new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.Name,
                Email = user.Email,
            };
        }


    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPatrol.Application.DTOs;
using PayPatrol.Application.Interfaces;
using PayPatrol.Infrastructure.Identity;

namespace PayPatrol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // API to test if the user is authenticated and have a cookie with a JWT token
        [HttpGet("me")]
        [Authorize] 
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var userExist = await _userManager.FindByEmailAsync(dto.Email);

            if(userExist != null)
            {
                return BadRequest("User with this email already exists.");
            }

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);
            SetJwtToken(token);

            return Ok(new {UserId = user.Id, Email = user.Email});
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user == null || user.Email == null)
            {
                return Unauthorized();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            if(user.Email == null)
            {
                return BadRequest();
            }

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);
            SetJwtToken(token);

            return Ok(new
            {
                Email = user.Email,
                UserId = user.Id
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(-1)
            };

            Response.Cookies.Append("jwt", "", cookieOptions);
            return Ok(new { Message = "Logged out successfully." });
        }

        private void SetJwtToken(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,  // JS cannot access the cookie
                Secure = true,    // Cookie is only sent over HTTPS
                SameSite = SameSiteMode.None,  // Cookie is sent on same-site requests and top-level navigation
                Expires = DateTimeOffset.UtcNow.AddMinutes(60),  // Set the expiration time for the cookie
                Path = "/"
            };

            Response.Cookies.Append("jwt", token, cookieOptions);
        }
    }
}

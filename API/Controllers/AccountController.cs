using API.DTOs.Request.Login;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Login endpoint
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                
               
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new { message = "Username and password are required" });
                }
                var result = await _accountService.Login(request);

                if (result.code ==200)
                {
                    return Ok(result);
                }

                return Unauthorized(new { success = result.message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Test protected endpoint
        /// </summary>
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirst("sub")?.Value;

            return Ok(new
            {
                success = true,
                username = username,
                userId = userId,
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }

    
}
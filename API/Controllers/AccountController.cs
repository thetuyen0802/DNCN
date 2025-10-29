using API.DTOs.Request.Login;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
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
            var res = JsonConvert.SerializeObject(await _accountService.Login(request));
            return Ok(res);
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
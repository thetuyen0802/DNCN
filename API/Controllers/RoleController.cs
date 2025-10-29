using Application.DTOs.Requests.Role;
using Application.DTOs.Responses.Role;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)     
        {
            _roleService = roleService;
        }

        [HttpPost("role")]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var response = JsonConvert.SerializeObject(  await _roleService.CreateRole(request));
            
            return Ok(response);

        }



    }
}

using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Service;

namespace TMS_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly ILogger _logger;

        public RoleController(AuthService authService, UserService userService, RoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _authService = authService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("getRoles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRoles();
            return Ok(response);
        }

    }
}

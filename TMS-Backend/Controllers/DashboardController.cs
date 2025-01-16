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
    public class DashboardController : Controller
    {
        private readonly AuthService _authService;
        private readonly DashboardService _dashboardService;
        private readonly UserService _userService;
        private readonly ILogger _logger;

        public DashboardController(AuthService authService, UserService userService, DashboardService dashboardService, ILogger<DashboardController> logger)
        {
            _authService = authService;
            _dashboardService = dashboardService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("searchDashboard")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SearchDashboard([FromBody] SearchDashboardViewModel searchModel)
        {
            var response = await _dashboardService.SearchDashboard(searchModel);
            return Ok(response);
        }


    }
}

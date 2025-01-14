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
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly ILogger _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Please input Username and Password");
            }

            var user = await _authService.CheckLogin(loginModel);

            if (user == null)
            {
                return NotFound("User not found");
            }
            var token = await _authService.GenerateJwtToken();
            return Ok(token);
        }


        [HttpGet]
        [Route("test")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult Report()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Populate the worksheet with some sample data
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[2, 1].Value = 1;
                worksheet.Cells[2, 2].Value = "John Doe";
                worksheet.Cells[3, 1].Value = 2;
                worksheet.Cells[3, 2].Value = "Jane Smith";

                // Convert the Excel package to a byte array
                var fileBytes = package.GetAsByteArray();

                // Return the file as a downloadable Excel file
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }
        }

    }
}

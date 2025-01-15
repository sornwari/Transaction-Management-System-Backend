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
    public class TransactionController : Controller
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;
        private readonly ILogger _logger;

        public TransactionController(AuthService authService, UserService userService, TransactionService transactionService, ILogger<TransactionController> logger)
        {
            _authService = authService;
            _userService = userService;
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("searchUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Login([FromBody] SearchUsersViewModel searchModel)
        {
            var response = await _userService.SearchUsers(searchModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("createUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserViewModel crateModel)
        {
            var response = await _userService.CreateUser(crateModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("updateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserViewModel updateModel)
        {
            var response = await _userService.UpdateUser(updateModel);
            return Ok(response);
        }

        [HttpGet]
        [Route("deleteUser/{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            var response = await _userService.DeleteUser(Id);
            return Ok(response);
        }

    }
}

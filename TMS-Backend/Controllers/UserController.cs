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
    public class UserController : Controller
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly ILogger _logger;

        public UserController(AuthService authService, UserService userService, ILogger<UserController> logger)
        {
            _authService = authService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("searchUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SearchUsers([FromBody] SearchUsersViewModel searchModel)
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

        [HttpPut]
        [Route("updateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserViewModel updateModel)
        {
            var response = await _userService.UpdateUser(updateModel);
            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteUser/{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            var response = await _userService.DeleteUser(Id);
            return Ok(response);
        }

    }
}

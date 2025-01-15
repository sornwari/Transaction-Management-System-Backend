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
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;
        private readonly ILogger _logger;

        public AccountController(AuthService authService, AccountService accountService, UserService userService, TransactionService transactionService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _authService = authService;
            _userService = userService;
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("searchAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SearchAccounts([FromBody] SearchAccountsViewModel searchModel)
        {
            var response = await _accountService.SearchAccounts(searchModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("createAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountViewModel crateModel)
        {
            var response = await _accountService.CreateAccount(crateModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("updateAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateAccount([FromBody] UpdateAccountViewModel updateModel)
        {
            var response = await _accountService.UpdateAccount(updateModel);
            return Ok(response);
        }

        [HttpGet]
        [Route("deleteAccount/{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteAccount(int Id)
        {
            var response = await _accountService.DeleteAccount(Id);
            return Ok(response);
        }

    }
}

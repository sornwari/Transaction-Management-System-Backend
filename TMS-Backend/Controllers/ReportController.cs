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
    public class ReportController : Controller
    {
        private readonly AuthService _authService;
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;
        private readonly ILogger _logger;

        public ReportController(AuthService authService, AccountService accountService, UserService userService, TransactionService transactionService, ILogger<ReportController> logger)
        {
            _accountService = accountService;
            _authService = authService;
            _userService = userService;
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("getReportUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetReportUser([FromBody] SearchUsersViewModel searchModel)
        {
            var response = await _userService.SearchUsers(searchModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("getReportAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetReportAccount([FromBody] SearchAccountsViewModel searchModel)
        {
            var response = await _accountService.SearchAccounts(searchModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("getReportTransaction")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetReportTransaction([FromBody] SearchTransactionsViewModel searchModel)
        {
            var response = await _transactionService.SearchTransactions(searchModel);
            return Ok(response);
        }

    }
}

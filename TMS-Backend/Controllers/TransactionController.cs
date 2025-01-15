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
        [Route("searchTransaction")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SearchTransactions([FromBody] SearchTransactionsViewModel searchModel)
        {
            var response = await _transactionService.SearchTransactions(searchModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("createTransaction")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionViewModel crateModel)
        {
            var response = await _transactionService.CreateTransaction(crateModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("updateTransaction")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateTransaction([FromBody] UpdateTransactionViewModel updateModel)
        {
            var response = await _transactionService.UpdateTransaction(updateModel);
            return Ok(response);
        }

        [HttpGet]
        [Route("deleteTransaction/{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteTransaction(int Id)
        {
            var response = await _transactionService.DeleteTransaction(Id);
            return Ok(response);
        }

    }
}

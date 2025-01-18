using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Service;
using System.Drawing;

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

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("User Report");

                //Header
                var header = new List<string>(new string[] { "No.", "Name", "Username", "Password", "Role Name" });
                for (int i = 0; i < header.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = header[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0066b1"));
                    worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheet.Cells[1, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                //Data
                for (int i = 2; i <= response.Count + 1; i++)
                {
                    worksheet.Cells["A" + i].Value = i - 1;
                    worksheet.Cells["B" + i].Value = response[i - 2].Name;
                    worksheet.Cells["C" + i].Value = response[i - 2].UserName;
                    worksheet.Cells["D" + i].Value = response[i - 2].Password;
                    worksheet.Cells["E" + i].Value = response[i - 2].Role.Name;
                }

                worksheet.Cells.AutoFitColumns();
                var fileBytes = package.GetAsByteArray();
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }

        }

        [HttpPost]
        [Route("getReportAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetReportAccount([FromBody] SearchAccountsViewModel searchModel)
        {
            var response = await _accountService.SearchAccounts(searchModel);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Account Report");

                //Header
                var header = new List<string>(new string[] { "No.", "AccountNo", "UserId", "Name", "Balance", "Deposit", "Withdraw", "Create Date" });
                for (int i = 0; i < header.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = header[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0066b1"));
                    worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheet.Cells[1, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                //Data
                for (int i = 2; i <= response.Count + 1; i++)
                {
                    worksheet.Cells["A" + i].Value = i - 1;
                    worksheet.Cells["B" + i].Value = response[i - 2].AccountNo;
                    worksheet.Cells["C" + i].Value = response[i - 2].UserId;
                    worksheet.Cells["D" + i].Value = response[i - 2].Name;
                    worksheet.Cells["E" + i].Value = response[i - 2].Balance;
                    worksheet.Cells["F" + i].Value = response[i - 2].Deposit;
                    worksheet.Cells["G" + i].Value = response[i - 2].Withdraw;
                    worksheet.Cells["H" + i].Value = response[i - 2].CreateDate.ToString("dd/MM/yyyy HH:mm:ss");
                }

                worksheet.Cells.AutoFitColumns();
                var fileBytes = package.GetAsByteArray();
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }
        }

        [HttpPost]
        [Route("getReportTransaction")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetReportTransaction([FromBody] SearchTransactionsViewModel searchModel)
        {
            var response = await _transactionService.SearchTransactions(searchModel);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Transaction Report");

                //Header
                var header = new List<string>(new string[] { "No.", "AccountNo", "Name", "TransactionName", "TransactionType", "TotalBeforeTransaction", "TotalAfterTransaction", "Amount", "Status", "Create Date" });
                for (int i = 0; i < header.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = header[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0066b1"));
                    worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheet.Cells[1, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                //Data
                for (int i = 2; i <= response.Count + 1; i++)
                {
                    worksheet.Cells["A" + i].Value = i - 1;
                    worksheet.Cells["B" + i].Value = response[i - 2].AccountNo;
                    worksheet.Cells["C" + i].Value = response[i - 2].Name;
                    worksheet.Cells["D" + i].Value = response[i - 2].TransactionName;
                    worksheet.Cells["E" + i].Value = response[i - 2].TransactionType;
                    worksheet.Cells["F" + i].Value = response[i - 2].TotalBeforeTransaction;
                    worksheet.Cells["G" + i].Value = response[i - 2].TotalAfterTransaction;
                    worksheet.Cells["H" + i].Value = response[i - 2].Amount;
                    worksheet.Cells["I" + i].Value = response[i - 2].Status;
                    worksheet.Cells["J" + i].Value = response[i - 2].CreateDate.ToString("dd/MM/yyyy HH:mm:ss");
                }

                worksheet.Cells.AutoFitColumns();
                var fileBytes = package.GetAsByteArray();
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }
        }

    }
}

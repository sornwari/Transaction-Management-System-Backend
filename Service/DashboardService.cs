using Database;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class DashboardService
    {
        private readonly TMSContext _db;
        private readonly IConfiguration _configuration;
        public DashboardService(TMSContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<DashboardViewModel> SearchDashboard(SearchDashboardViewModel searchModel)
        {
            try
            {
                DateTime utcDateTimeFrom = new DateTime();
                DateTime utcDateTimeTo = new DateTime();
                if (searchModel.FromDate != null)
                {
                    utcDateTimeFrom = DateTime.SpecifyKind(searchModel.FromDate.Value, DateTimeKind.Utc);
                }
                if(searchModel.ToDate != null)
                {
                    utcDateTimeTo = DateTime.SpecifyKind(searchModel.ToDate.Value, DateTimeKind.Utc);
                }

                var result = new DashboardViewModel();

                var totalDeposit = await _db.Transaction
                    .Where(x => x.TransactionType == "Deposit"
                    && (string.IsNullOrEmpty(searchModel.Status) || x.Status == searchModel.Status)
                    && (searchModel.FromDate == null || x.CreateDate >= utcDateTimeFrom)
                    && (searchModel.ToDate == null || x.CreateDate <= utcDateTimeTo))
                    .SumAsync(x => x.Amount);

                var totalWithdraw = await _db.Transaction
                    .Where(x => x.TransactionType == "Withdraw"
                    && (string.IsNullOrEmpty(searchModel.Status) || x.Status == searchModel.Status)
                    && (searchModel.FromDate == null || x.CreateDate >= utcDateTimeFrom)
                    && (searchModel.ToDate == null || x.CreateDate <= utcDateTimeTo))
                    .SumAsync(x => x.Amount);

                var totalBalance = totalDeposit - totalWithdraw;

                result.TotalDeposit = totalDeposit;
                result.TotalWithdraw = totalWithdraw;
                result.TotalBalance = totalBalance;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

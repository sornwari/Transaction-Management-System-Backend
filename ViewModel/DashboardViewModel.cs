using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class DashboardViewModel
    {
        public decimal TotalBalance { get; set; }
        public decimal TotalDeposit { get; set; }
        public decimal TotalWithdraw { get; set; }

    }
    public class SearchDashboardViewModel
    {
        public string Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }


    }

    public class SearchTransactionsViewModel
    {
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
    public class CreateTransactionViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string CreateBy { get; set; }

    }
    public class UpdateTransactionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string UpdateBy { get; set; }

    }
}

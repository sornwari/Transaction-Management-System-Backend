using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalBeforeTransaction { get; set; }
        public decimal TotalAfterTransaction { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class SearchTransactionsViewModel
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
    public class CreateTransactionViewModel
    {
        public string AccountNo { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
    }

    public class UpdateTransactionViewModel
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string UpdateBy { get; set; }

    }
}

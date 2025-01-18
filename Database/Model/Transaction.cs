using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalBeforeTransaction { get; set; }
        public decimal TotalAfterTransaction { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }

    }
}

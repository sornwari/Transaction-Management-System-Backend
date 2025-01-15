using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdraw { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }

    }
}

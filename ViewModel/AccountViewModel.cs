using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdraw { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class SearchAccountsViewModel
    {
        public string AccountNo { get; set; }
        public string Name { get; set; }
    }
    public class CreateAccountViewModel
    {
        public string AccountNo { get; set; }
        public int UserId { get; set; }
        public string CreateBy { get; set; }

    }
    public class UpdateAccountViewModel
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public int UserId { get; set; }
        public string UpdateBy { get; set; }

    }

}

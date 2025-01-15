using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleViewModel Role { get; set; }

    }

    public class SearchUsersViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string CreateBy { get; set; }

    }
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string UpdateBy { get; set; }

    }
}

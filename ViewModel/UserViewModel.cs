using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class LoginSuccessViewModel
    {
        public UserViewModel User { get; set; }
        public string Token { get; set; }

    }

}

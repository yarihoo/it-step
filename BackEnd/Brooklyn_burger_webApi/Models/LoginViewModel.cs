namespace Internet_Market_WebApi.Models
{

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}

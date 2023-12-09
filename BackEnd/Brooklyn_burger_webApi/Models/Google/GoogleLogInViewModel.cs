namespace Internet_Market_WebApi.Models.Google
{
    public class GoogleLogInViewModel
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadImage { get; set; }

    }
}

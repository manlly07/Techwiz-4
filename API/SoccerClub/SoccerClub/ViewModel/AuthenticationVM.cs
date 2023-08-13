namespace SoccerClub.ViewModel
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }

    }
    public class LoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ApiResponseVM
    {
        public bool Success { get; set; }   
        public string Error { get; set; }
        public object data { get; set; }
    }
    public class TokenVM
    {
        public string AcctessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class RefreshTokenVm
    {
        public Guid UserID { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}


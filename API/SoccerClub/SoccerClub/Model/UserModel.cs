namespace SoccerClub.Model
{
    public class UserModel
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate  { get; set; }
        public DateTime ModifeDate { get; set; }
        public string Avatar { get; set; }
        public int RoleID { get; set; }
    }
}


namespace SoccerClub.ViewModel
{
    public class PlayerInsertVM
    {
        public string PlayerName { get; set; }
        public DateTime BirthDay { get; set; }
        public int CountryID { get; set; }
        public string Description { get; set; }
        public string Achievements { get; set; }
    }
    public class PlayerUpdateVM
    {
        public Guid PlayerID { get; set; }
        public string PlayerName { get; set; }
        public DateTime BirthDay { get; set; }
        public int CountryID { get; set; }
        public string Description { get; set; }
        public string Achievements { get; set; }

    }
    public class PlayerSearchVM : SearchRequestDateVM
    {

    }
    public class GetPlayerVM
    {
        public Guid PlayerID { get; set; }
        public string PlayerName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Achievements { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

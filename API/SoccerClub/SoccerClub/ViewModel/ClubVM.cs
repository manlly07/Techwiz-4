
namespace SoccerClub.ViewModel
{
    public class ClubInsertVM
    {
        public string ClubName { get; set; }
        public int CountryID { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime Founding { get; set; }  

    }
    public class ClubUpdateVM
    {
        public Guid ClubID { get; set; }
        public string ClubName { get; set; }
        public int CountryID { get; set; }
        public string Logo { get; set; }
        public DateTime Founding { get; set; }
        public string Description { get; set; }


    }
    public class ClubSearchVM : SearchRequestDateVM
    {

    }
    public class GetClubVM
    {
        public Guid ClubID { get; set; }
        public string ClubName { get; set; }
        public int CountryID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Logo { get; set; }
        public DateTime Founding { get; set; }

    }
}

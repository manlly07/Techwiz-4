
namespace SoccerClub.ViewModel
{
    public class CountryInsertVM
    {
        public string CountryName { get; set; }
        public string Description { get; set; }

    }
    public class CountryUpdateVM
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }

    }
    public class CountrySearchVM : SearchRequestVM
    {

    }
    public class GetCountryVM
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
    }
}

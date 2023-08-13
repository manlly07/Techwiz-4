namespace SoccerClub.Model
{
    public class ClubModel
    {
        public Guid ClubID { get; set; }
        public string ClubName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifeDate { get; set; }
        public int CountryID { get; set; }
    }
}

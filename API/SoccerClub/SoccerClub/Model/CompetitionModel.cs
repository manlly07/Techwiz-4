namespace SoccerClub.Model
{
    public class CompetitionModel
    {
        public Guid CompetitonID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifeDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CompetitionName { get; set; }
        public string AddressEvent { get; set; }
        public int CountryID { get; set; }
    }
}

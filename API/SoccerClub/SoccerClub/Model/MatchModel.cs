namespace SoccerClub.Model
{
    public class MatchModel
    {
        public int ID { get; set; }
        public Guid MatchID { get; set; }
        public DateTime MatchTime { get; set; }
        public DateTime  ModifeDate{ get; set; }
        public string ClubA { get; set; }
        public string ClubB { get; set;}
        public Guid CompetitionID { get; set; }
        public int Status { get; set; }
        public string Referee { get; set; }
    }
}

namespace SoccerClub.Model
{
    public class MatchStatisticModel
    {
        public int ID { get; set; }
        public Guid MatchID { get; set; }
        public Guid PlayerID { get; set; }
        public DateTime Time { get; set; }
        public Guid ClubID { get; set; }
    }
}

using System.Security.Principal;

namespace SoccerClub.Model
{
    public class MatchDetailModel
    {
        public Guid MatchDetailID { get; set; }
        public int ScoreClubA {get; set;}
        public int ScoreClubB { get; set;}
        public int ID { get; set; }
        public int TotalTime { get; set; }
        public Guid MatchID { get; set; }
    }
}

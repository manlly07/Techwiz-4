namespace SoccerClub.ViewModel
{
    public class MatchInsertVM
    {
        public DateTime MatchTime { get; set; }
        public Guid CompetitorA { get; set; }
        public Guid CompetitorB { get; set; }
        public Guid CompetitionID { get; set; }
        public int Status { get; set; }
        public Guid RefereeID { get; set; }
        public string Title { get; set; }
        public int ScoreCompetitorA { get; set; }
        public int ScoreCompetitorB { get; set; }
        public string Description { get; set; }
    }
    public class MatchUpdateVM
    {
        public Guid MatchID { get; set; }
        public DateTime MatchTime { get; set; }
        public Guid CompetitorA { get; set; }
        public Guid CompetitorB { get; set; }
        public Guid CompetitionID { get; set; }
        public int Status { get; set; }
        public Guid RefereeID { get; set; }
        public string Title { get; set; }
        public int ScoreCompetitorA { get; set; }
        public int ScoreCompetitorB { get; set; }
        public string Description { get; set; }
    }
    public class MatchSearchVM : SearchRequestDateVM
    {

    }
    public class GetMatchVM
    {
        public Guid MatchID { get; set; }
        public DateTime MatchTime { get; set; }
        public string CompetitorA { get; set; }
        public string CompetitorB { get; set; }
        public string CompetitionName { get; set; }
        public int ScoreCompetitorA { get; set; }
        public int ScoreCompetitorB { get; set; }
        public int Status { get; set; }
        public string RefereeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<RefereeTypeVM> RefereeTypes { get; set; }
    }
}

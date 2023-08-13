namespace SoccerClub.Model
{
    public class FeedbackModel
    {
        public int FeedBackID { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set;}
        public int Vote { get; set; }
        public int UserId { get; set; }
        public string Tiltle { get; set; }
    }
}

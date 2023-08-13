namespace SoccerClub.ViewModel
{
    public class FeedbackInsertVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Vote { get; set; }
        public Guid UserID { get; set; }
    }
    //public class FeedbackUpdateVM
    //{
    //    public int CountryID { get; set; }
    //    public string Title { get; set; }
    //    public string Content { get; set; }
    //    public int Vote { get; set; }
    //    public Guid UserID { get; set; }
    //}
    public class FeedbackSearchVM : SearchRequestDateVM
    {

    }
    public class GetFeedbackVM
    {
        public int FeedBackID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Vote { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

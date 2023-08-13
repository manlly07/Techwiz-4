
namespace SoccerClub.Model
{
    public class ArticleModel
    {
        public Guid ArticleID { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifeDate { get; set; }
        public string Title { get; set; }
        public bool IsDelete { get; set; }
    }
}

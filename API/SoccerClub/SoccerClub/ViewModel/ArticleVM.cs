
namespace SoccerClub.ViewModel
{
    public class ArticleInsertVM
    {
        public string Content { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class ArticleUpdateVM
    {
        public Guid ArticleID { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    public class ArticleSearchVM : SearchRequestDateVM
    {

    }
    public class GetArticleVM
    {
        public Guid ArticleID { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

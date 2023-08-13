
namespace SoccerClub.ViewModel
{
    public class CategoryInsertVM
    {
        public string CategoryName { get; set; }
        public Guid ParentID { get; set; } = Guid.Empty;

    }
    public class CategoryUpdateVM
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid ParentID { get; set; } = Guid.Empty;
    }
    public class CategorySearchVM : SearchRequestVM
    {

    }
    public class GetCategoryVM
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

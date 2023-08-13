
namespace SoccerClub.ViewModel
{
    public class OrderInsertVM
    {
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderUpdateVM
    {
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderSearchVM : SearchRequestDateVM
    {

    }
    public class GetOrderVM
    {
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

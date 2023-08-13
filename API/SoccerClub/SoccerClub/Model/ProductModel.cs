namespace SoccerClub.Model
{
    public class ProductModel
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifeDate { get; set; }
        public Decimal Price { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Avatar { get; set; }
        public int ID { get; set; }
    }
}

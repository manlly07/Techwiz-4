namespace SoccerClub.Model
{
    public class PlayerModel
    {
        public Guid PlayerID { get; set; }
        public string PlayerName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime CreateDate { get; set;}
        public DateTime ModifeDate { get; set; }
        public int ID { get; set; }
        public string Achivements { get; set; }
    }
}

namespace SoccerClub.Model
{
    public class SystemkeyModel
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string CodeKey { get; set; }
        public int CodeValue { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}

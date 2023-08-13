using System.ComponentModel;

namespace SoccerClub.ViewModel
{
    public class SearchRequestVM
    {
        [DefaultValue(50)]
        public int PageSize { get; set; } = 50;

        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;

        [DefaultValue("")]
        public string TextSearch { get; set; } = string.Empty;
    }
    public class SearchRequestDateVM
    {
        [DefaultValue(50)]
        public int PageSize { get; set; } = 50;

        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;

        [DefaultValue("")]
        public string TextSearch { get; set; } = string.Empty;

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class PagingVM
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
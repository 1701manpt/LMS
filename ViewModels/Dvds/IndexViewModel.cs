using LMS.Models;
using LMS.Views.Shared.PartialViews;

namespace LMS.ViewModels.Dvds
{
    public class IndexViewModel
    {
        public List<Dvd>? Dvds { get; set; }
        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}

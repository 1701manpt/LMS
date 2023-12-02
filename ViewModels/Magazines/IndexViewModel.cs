using LMS.Models;
using LMS.Views.Shared.PartialViews;

namespace LMS.ViewModels.Magazines
{
    public class IndexViewModel
    {
        public IEnumerable<Magazine>? Magazines { get; set; }
        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}

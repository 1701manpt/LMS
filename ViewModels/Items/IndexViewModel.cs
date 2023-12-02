using LMS.Models;
using LMS.Views.Shared.PartialViews;

namespace LMS.ViewModels.Items
{
    public class IndexViewModel
    {
        public string? Title { get; set; }
        public IEnumerable<Item>? Items { get; set; }
        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}

using LMS.Models;
using LMS.Views.Shared.PartialViews;

namespace LMS.ViewModels.Books
{
    public class IndexViewModel
    {
        public IEnumerable<Book>? Books { get; set; }
        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}

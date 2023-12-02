using LMS.Models;
using LMS.Views.Shared.PartialViews;

namespace LMS.ViewModels.Borrowers
{
    public class IndexViewModel
    {
        public IEnumerable<Borrower>? Borrowers { get; set; }

        public string? LibraryCardNumber { get; set; }

        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}

using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels.BorrowedHistories
{
    public class CreateViewModel
    {
        [Display(Name = "Borrower")]
        public int BorrowerId { get; set; }

        [Display(Name = "Borrowed Date")]
        public required string BorrowedDate { get; set; }

        [Display(Name = "Total Cost")]
        public required string TotalCost { get; set; }

        public IEnumerable<SelectListItem>? BorrowerSelectList { get; set; }

        public IEnumerable<BorrowedItemTemp>? BorrowedItemTemps { get; set; }
    }
}

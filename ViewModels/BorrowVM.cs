using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class BorrowVM
    {
        public BorrowedItemTemp BorrowedItemTemp { get; set; }

        public BorrowedHistory BorrowedHistory { get; set; }

        [Display(Name = "#")]
        public int Index { get; set; }
    }
}

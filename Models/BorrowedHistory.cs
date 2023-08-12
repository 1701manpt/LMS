using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    // Model lịch sử mượn sách
    public class BorrowedHistory
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Borrower")]
        public int BorrowerId { get; set; } // Khóa ngoại tới Borrower

        [Display(Name = "Borrowed Date")]
        public DateTime BorrowedDate { get; set; }

        [Display(Name = "Total Cost")]
        [Range(1, double.MaxValue)]
        public decimal? TotalCost { get; set; }

        public Borrower? Borrower { get; set; } // Navigation property đến Borrower
        public List<BorrowedItem>? BorrowedItems { get; set; }
    }
}

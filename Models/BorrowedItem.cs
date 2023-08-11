using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class BorrowedItem
    {
        public int Id { get; set; }

        public int ItemId { get; set; } // Khóa ngoại tới Item

        public int BorrowHistoryId { get; set; } // Khóa ngoại tới BorrowedHistory

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Cost { get; set; }


        public Item? Item { get; set; } // Navigation property đến Item
        public BorrowedHistory? BorrowedHistory { get; set; }
    }
}

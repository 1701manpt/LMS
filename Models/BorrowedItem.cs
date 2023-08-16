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

        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(
            @"^(500|[1-9]\d*000)$",
            ErrorMessage = "The field Cost is invalid."
        )]
        public decimal Cost { get; set; }


        public Item? Item { get; set; } // Navigation property đến Item
        public BorrowedHistory? BorrowedHistory { get; set; }
    }
}

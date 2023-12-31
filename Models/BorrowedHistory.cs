﻿using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        public DateTime BorrowedDate { get; set; }

        [Display(Name = "Total Cost")]
        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(
            @"^(500|[1-9]\d*000)$",
            ErrorMessage = "The field Total Cost is invalid."
        )]
        public decimal TotalCost { get; set; }

        [Display(Name = "Borrowed State")]
        public BorrowedState? BorrowedState { get; set; }

        public Borrower? Borrower { get; set; } // Navigation property đến Borrower
        public List<BorrowedItem>? BorrowedItems { get; set; }
    }
}

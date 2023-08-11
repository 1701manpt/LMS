using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class BorrowedItemsVM
    {
        [Display(Name = "Index")]
        public int Id { get; set; }

        [Display(Name = "Item")]
        public required int ItemId { get; set; }
        public Item? Item { get; set; }
        public int? BorrowHistoryId { get; set; }
        public BorrowedHistory? BorrowedHistory { get; set; }

        [Display(Name = "Quantity")]
        public required int Quantity { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }
    }
}

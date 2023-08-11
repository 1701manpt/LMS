using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class BorrowedItemTemp
    {
        public int Id { get; set; }

        [Display(Name = "Item")]
        public int ItemId { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Cost")]
        [Range(0.01, double.MaxValue)]
        public decimal Cost { get; set; }

        public Item? Item { get; set; }
    }
}

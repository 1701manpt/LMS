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
        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:F0}")]
        [RegularExpression(
            @"^(500|[1-9]\d*000)$",
            ErrorMessage = "The field Cost is invalid."
        )]
        public decimal? Cost { get; set; } // sẽ không lưu dữ liệu // chỉ có borrowedItem mới lưu

        public Item? Item { get; set; }
    }
}

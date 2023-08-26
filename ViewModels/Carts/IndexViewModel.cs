using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels.Carts
{
    public class IndexViewModel
    {
        [Display(Name = "Borrowed Date")]
        [DataType(DataType.Date)]
        public DateTime BorrowedDate { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        public IEnumerable<CartItemTemp>? CartItemTempList { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Borrower
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public required string Name { get; set; }

        [Display(Name = "Address")]
        public required string Address { get; set; }

        [Display(Name = "Library Card Number")]
        public required string LibraryCardNumber { get; set; }

        public List<BorrowedHistory>? BorrowedHistories { get; set; }
        public List<Cart>? Carts { get; set; }
        public List<CartItemTemp>? CartItemTemp { get; set; }
    }
}

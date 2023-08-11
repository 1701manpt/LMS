using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    // Model Borrower
    public class Borrower
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }


        public List<BorrowedHistory>? BorrowedHistories { get; set; }
    }
}

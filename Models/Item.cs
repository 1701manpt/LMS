using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    // Model Item chung cho Book, Dvd, Magazine
    public abstract class Item
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Type")]
        public ItemType Type { get; }

        [Display(Name = "Title")]
        public required string Title { get; set; }

        [Display(Name = "Author")]
        public required string Author { get; set; }

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Price")]
        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(
            @"^(500|[1-9]\d*000)$",
            ErrorMessage = "The field Price is invalid."
        )]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Available Quantity")]
        public int AvailableQuantity { get; set; }

        public List<BorrowedItem>? BorrowedItems { get; set; }
        public List<BorrowedItemTemp>? BorrowedItemTemps { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<CartItemTemp>? CartItemTemps { get; set; }
    }
}

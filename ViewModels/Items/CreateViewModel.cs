using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels.Items
{
    public class CreateViewModel
    {
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

        // Book
        [Display(Name = "Number Of Pages")]
        [Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }

        // DVD
        [Display(Name = "Run Time")]
        [Range(1, int.MaxValue)]
        public int RunTime { get; set; }
    }
}

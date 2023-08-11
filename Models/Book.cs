using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Book : Item
    {
        [Display(Name = "Number Of Pages")]
        [Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }
    }
}

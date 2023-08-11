using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    // Model DVD kế thừa từ Item
    public class DVD : Item
    {
        [Display(Name = "Run Time")]
        [Range(1, int.MaxValue)]
        public int RunTime { get; set; }
    }
}

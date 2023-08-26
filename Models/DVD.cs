using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    // Model Dvd kế thừa từ Item
    public class Dvd : Item
    {
        [Display(Name = "Run Time")]
        [Range(1, int.MaxValue)]
        public int RunTime { get; set; }
    }
}

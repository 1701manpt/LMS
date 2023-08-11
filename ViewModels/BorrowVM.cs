using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class BorrowVM
    {
        private readonly AppDbContext _context;

        public BorrowedItem BorrowedItem { get; set; }

        public BorrowedHistory BorrowedHistory { get; set; }

        public string getTitleItem(int itemId)
        {
            string title = _context.Items.Find(itemId).Title;
            return title;
        }

        [Display(Name = "#")]
        public int Index { get; set; }
    }
}

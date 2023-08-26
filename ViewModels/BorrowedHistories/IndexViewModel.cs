using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels.BorrowedHistories
{
    public class IndexViewModel
    {
        public required IEnumerable<BorrowedHistory> BorrowedHistories { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public int? ItemId { get; set; }

        public int? BorrowerId { get; set; }

        public BorrowedState? BorrowedState { get; set; }

        public required IEnumerable<SelectListItem> ItemSelectList { get; set; }

        public required IEnumerable<SelectListItem> BorrowerSelectList { get; set; }

        public IEnumerable<SelectListItem> StateSelectList { get; set; }
    }
}

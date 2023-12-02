using LMS.Models;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels.BorrowedHistories
{
    public class IndexViewModel
    {
        public required IEnumerable<BorrowedHistory> BorrowedHistories { get; set; }

        [DataType(DataType.Date)]
        public string? StartDate { get; set; }

        [DataType(DataType.Date)]
        public string? EndDate { get; set; }

        public int? ItemId { get; set; }

        public int? BorrowerId { get; set; }

        public BorrowedState? BorrowedState { get; set; }

        public IEnumerable<SelectListItem>? ItemSelectList { get; set; }

        public IEnumerable<SelectListItem>? BorrowerSelectList { get; set; }

        public IEnumerable<SelectListItem>? StateSelectList { get; set; }

        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}

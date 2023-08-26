using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class BorrowedItemsController : Controller
    {
        private readonly IBorrowedItemService _borrowedItemService;

        public BorrowedItemsController(IBorrowedItemService borrowedItemService)
        {
            _borrowedItemService = borrowedItemService;
        }

        public IActionResult Create(int id)
        {
            try
            {
                var borrowedHistoryId = id;

                _borrowedItemService.CreateBorrowedItems(borrowedHistoryId);

                return RedirectToAction("Index", "BorrowedHistories");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

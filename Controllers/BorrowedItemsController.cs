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

        public IActionResult Create(int borrowedHistoryId)
        {
            try
            {
                _borrowedItemService.CreateBorrowedItems(borrowedHistoryId);

                return RedirectToAction("Index", "BorrowedHistories");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Return(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            if (!_borrowedItemService.Exist((int)id))
            {
                return NotFound();
            }

            _borrowedItemService.Return((int)id);

            var borrowedItem = _borrowedItemService.Details((int)id);

            return RedirectToAction("Details", "BorrowedHistories", new {id = borrowedItem.BorrowedHistoryId});
        }
    }
}

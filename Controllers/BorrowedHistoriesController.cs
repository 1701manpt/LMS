using LMS.Models;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LMS.ViewModels.BorrowedHistories;
using System.Runtime.InteropServices;

namespace LMS.Controllers
{
    public class BorrowedHistoriesController : Controller
    {
        private readonly IBorrowedHistoryService _borrowedHistoryService;
        private readonly IBorrowerService _borrowerService;
        private readonly IBorrowedItemTempService _borrowedItemTempService;
        private readonly IItemService _itemService;

        public BorrowedHistoriesController(IBorrowedHistoryService borrowedHistoryService, IBorrowerService borrowerService, IBorrowedItemTempService borrowedItemTempService, IItemService itemService)
        {
            _borrowedHistoryService = borrowedHistoryService;
            _borrowerService = borrowerService;
            _borrowedItemTempService = borrowedItemTempService;
            _itemService = itemService;
        }

        // GET: BorrowedHistories
        public IActionResult Index(int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState)
        {
            try
            {
                IndexViewModel indexViewModel = new IndexViewModel
                {
                    BorrowedHistories = _borrowedHistoryService.Index(),
                    BorrowerSelectList = new SelectList(_borrowerService.Index(), "Id", "LibraryCardNumber"),
                    ItemSelectList = new SelectList(_itemService.Index(), "Id", "Title"),
                };

                if (borrowerId != null || itemId != null || startDate != null || endDate != null || borrowedState != null)
                {
                    indexViewModel.BorrowedHistories = _borrowedHistoryService.Search(borrowerId, itemId, startDate, endDate, borrowedState);
                }

                if (startDate != null)
                {
                    indexViewModel.StartDate = (DateTime)startDate;
                }

                if (endDate != null)
                {
                    indexViewModel.EndDate = (DateTime)endDate;
                }

                var borrowedStateValues = Enum.GetValues(typeof(BorrowedState));
                var stateSelectList = new List<SelectListItem>();
                for (int i = 0; i < borrowedStateValues.Length; i++)
                {
                    stateSelectList.Add(new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = borrowedStateValues.GetValue(i).ToString()
                    });
                }

                indexViewModel.StateSelectList = stateSelectList;

                return View(indexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Search(IndexViewModel indexViewModel)
        {
            return RedirectToAction("Index", new
            {
                startDate = indexViewModel.StartDate,
                endDate = indexViewModel.EndDate,
                borrowerId = indexViewModel.BorrowerId,
                itemId = indexViewModel.ItemId,
                borrowedState = indexViewModel.BorrowedState
            });
        }

        // GET: BorrowedHistories/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowedHistoryService.Exist((int)id))
                {
                    return NotFound();
                }

                var borrowedHistory = _borrowedHistoryService.Details((int)id);

                return View(borrowedHistory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SearchBorrowers()
        {
            try
            {
                return Redirect("Create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: BorrowedHistories/Create
        public IActionResult Create()
        {
            try
            {
                var borrowerSelectList = _borrowerService.Index()
                   .Select(b => new
                   {
                       Value = b.Id,
                       Text = $"{b.LibraryCardNumber} - {b.Name}"
                   });
                ViewData["BorrowerId"] = new SelectList(borrowerSelectList, "Value", "Text");
                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                ViewData["BorrowedDate"] = currentDate;

                var borrowedItemTempList = _borrowedItemTempService.Index();

                ViewData["BorrowedItemTemps"] = borrowedItemTempList;

                decimal totalCost = _borrowedHistoryService.CalcTotalCost();

                ViewData["TotalCost"] = totalCost.ToString("F0");

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: BorrowedHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BorrowerId,BorrowedDate,TotalCost")] BorrowedHistory borrowedHistory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _borrowedHistoryService.Create(borrowedHistory);

                    return RedirectToAction("Create", "BorrowedItems", new { id = borrowedHistory.Id });
                }
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: BorrowedHistories/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowedHistoryService.Exist((int)id))
                {
                    return NotFound();
                }

                var borrowedHistory = _borrowedHistoryService.Details((int)id);

                return View(borrowedHistory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: BorrowedHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _borrowedHistoryService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

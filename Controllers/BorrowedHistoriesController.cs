using LMS.Models;
using LMS.Services.Interfaces;
using LMS.ViewModels.BorrowedHistories;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Globalization;

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
        [HttpGet]
        public IActionResult Index(int? pageNumber, int? pageSize, int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState)
        {
            try
            {
                IndexViewModel indexViewModel = new IndexViewModel
                {
                    BorrowedHistories = _borrowedHistoryService.Index(),
                    BorrowerSelectList = new SelectList(_borrowerService.Index(), "Id", "LibraryCardNumber"),
                    ItemSelectList = new SelectList(_itemService.Index(), "Id", "Title"),
                };

                var borrowedHitoriesQuery = _borrowedHistoryService.GetAll();

                //if (borrowerId != null || itemId != null || startDate != null || endDate != null || borrowedState != null)
                //{
                    borrowedHitoriesQuery = _borrowedHistoryService.Search
                        (
                            borrowedHitoriesQuery,
                            borrowerId,
                            itemId,
                            startDate,
                            endDate,
                            borrowedState
                        );
                //}

                if (!pageNumber.HasValue)
                {
                    pageNumber = 1;
                }

                if (!pageSize.HasValue)
                {
                    pageSize = 2;
                }

                PaginationPartialViewModel pagination = new()
                {
                    PageSize = (int)pageSize,
                    CurrentPage = (int)pageNumber,
                    TotalPages = _borrowedHistoryService.CountPage(borrowedHitoriesQuery, (int)pageSize)
                };

                ViewData["BorrowerId"] = borrowerId;
                ViewData["ItemId"] = itemId;
                ViewData["BorrowedState"] = borrowedState;
                ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
                ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

                indexViewModel.PaginationPartialViewModel = pagination;

                borrowedHitoriesQuery = _borrowedHistoryService.GetPageByNumberPage(borrowedHitoriesQuery, (int)pageNumber, (int)pageSize);

                indexViewModel.BorrowedHistories = borrowedHitoriesQuery.ToList();

                if (!startDate.HasValue)
                {
                    indexViewModel.StartDate = startDate?.ToString("yyyy-MM-dd");
                }

                if (!endDate.HasValue)
                {
                    indexViewModel.EndDate = endDate?.ToString("yyyy-MM-dd");
                }

                var borrowedStateValues = Enum.GetValues(typeof(BorrowedState));
                var borrowedStates = new List<SelectListItem>();

                for (int i = 0; i < borrowedStateValues.Length; i++)
                {
                    borrowedStates.Add(new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = borrowedStateValues.GetValue(i).ToString()
                    });
                }

                indexViewModel.StateSelectList = new SelectList(borrowedStates, "Value", "Text");

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

        // GET: BorrowedHistories/Create
        public IActionResult Create()
        {
            try
            {
                CreateViewModel createViewModel = new CreateViewModel
                {
                    BorrowedDate = DateTime.Now.ToString("dd-MM-yyyy"),
                    TotalCost = _borrowedHistoryService.CalcTotalCost().ToString("C"),
                };

                var borrowers = _borrowerService.Index()
                   .Select(b => new
                   {
                       Value = b.Id,
                       Text = $"{b.LibraryCardNumber} - {b.Name}"
                   });

                createViewModel.BorrowerSelectList = new SelectList(borrowers, "Value", "Text");

                createViewModel.BorrowedItemTemps = _borrowedItemTempService.Index();

                return View(createViewModel);
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
        public IActionResult Create([Bind("Id,BorrowerId,BorrowedDate,TotalCost")] CreateViewModel createViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var borrowedHistory = new BorrowedHistory
                    {
                        BorrowerId = createViewModel.BorrowerId,
                        BorrowedDate = DateTime.Now,
                        TotalCost = _borrowedHistoryService.CalcTotalCost()
                    };

                    _borrowedHistoryService.Create(borrowedHistory);

                    return RedirectToAction("Create", "BorrowedItems", new { borrowedHistoryId = borrowedHistory.Id });
                }

                var borrowers = _borrowerService.Index()
                   .Select(b => new
                   {
                       Value = b.Id,
                       Text = $"{b.LibraryCardNumber} - {b.Name}"
                   });

                createViewModel.BorrowerSelectList = new SelectList(borrowers, "Value", "Text");

                return View(createViewModel);
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

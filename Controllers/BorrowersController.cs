using LMS.Models;
using LMS.Services.Interfaces;
using LMS.ViewModels.Borrowers;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        // GET: Borrowers
        public IActionResult Index(int? pageNumber, int? pageSize, string? libraryCardNumber)
        {
            try
            {
                IndexViewModel indexViewModel = new IndexViewModel();

                var borrowersQuery = _borrowerService.GetAll();

                if(!string.IsNullOrEmpty(libraryCardNumber))
                {
                    indexViewModel.LibraryCardNumber = libraryCardNumber;
                    ViewData["LibraryCardNumber"] = libraryCardNumber;
                    borrowersQuery = _borrowerService.Search(borrowersQuery, libraryCardNumber);
                }

                if (!Convert.ToBoolean(pageNumber))
                {
                    pageNumber = 1;
                }

                if (!Convert.ToBoolean(pageSize))
                {
                    pageSize = 2;
                }

                PaginationPartialViewModel pagination = new PaginationPartialViewModel
                {
                    PageSize = (int)pageSize,
                    CurrentPage = (int)pageNumber,
                    TotalPages = _borrowerService.CountPage(borrowersQuery, (int)pageSize)
                };

                indexViewModel.PaginationPartialViewModel = pagination;

                borrowersQuery = _borrowerService.Pagination(borrowersQuery, (int)pageNumber, (int)pageSize);

                indexViewModel.Borrowers = borrowersQuery.ToList();

                return View(indexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public JsonResult List(int? pageNumber, int? pageSize, string? libraryCardNumber)
        {
            try
            {
                IndexViewModel indexViewModel = new IndexViewModel();

                var borrowersQuery = _borrowerService.GetAll();

                if (!string.IsNullOrEmpty(libraryCardNumber))
                {
                    indexViewModel.LibraryCardNumber = libraryCardNumber;
                    ViewData["LibraryCardNumber"] = libraryCardNumber;
                    borrowersQuery = _borrowerService.Search(borrowersQuery, libraryCardNumber);
                }

                if (!Convert.ToBoolean(pageNumber))
                {
                    pageNumber = 1;
                }

                if (!Convert.ToBoolean(pageSize))
                {
                    pageSize = 2;
                }

                PaginationPartialViewModel pagination = new PaginationPartialViewModel
                {
                    PageSize = (int)pageSize,
                    CurrentPage = (int)pageNumber,
                    TotalPages = _borrowerService.CountPage(borrowersQuery, (int)pageSize)
                };

                indexViewModel.PaginationPartialViewModel = pagination;

                borrowersQuery = _borrowerService.Pagination(borrowersQuery, (int)pageNumber, (int)pageSize);

                indexViewModel.Borrowers = borrowersQuery.ToList();

                return Json(indexViewModel.Borrowers);
            }
            catch(Exception ex)
            {
                return Json(BadRequest(ex));
            }
        }

        [HttpPost]
        public IActionResult Search(string? libraryCardNumber)
        {
            return RedirectToAction("Index", new { libraryCardNumber });
        }

        // GET: Borrowers/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowerService.Exist((int)id))
                {
                    return NotFound();
                }

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult DetailsJson(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Json(NotFound());
                }

                if (!_borrowerService.Exist((int)id))
                {
                    return Json(NotFound());
                }

                var borrower = _borrowerService.Details((int)id);
               
                return Json(borrower);
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        // GET: Borrowers/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Address,LibraryCardNumber")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                _borrowerService.Create(borrower);

                return RedirectToAction(nameof(Index));
            }
            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowerService.Exist((int)id))
                {
                    return NotFound();
                }

                var borrower = _borrowerService.Details((int)id);

                return View(borrower);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,LibraryCardNumber")] Borrower borrower)
        {
            try
            {
                if (id != borrower.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _borrowerService.Edit(borrower);
                    return RedirectToAction(nameof(Index));
                }

                return View(borrower);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Borrowers/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowerService.Exist((int)id))
                {
                    return NotFound();
                }

                var borrower = _borrowerService.Details((int)id);

                return View(borrower);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _borrowerService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

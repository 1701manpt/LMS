using LMS.Models;
using LMS.Services.Interfaces;
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
        public IActionResult Index(string? libraryCardNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(libraryCardNumber))
                {
                    ViewData["LibraryCardNumber"] = libraryCardNumber;
                    return View(_borrowerService.Search(libraryCardNumber));
                }


                return View(_borrowerService.Index());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

                var borrower = _borrowerService.Details((int)id);

                return View(borrower);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Borrowers/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
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
            catch (Exception ex) {
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

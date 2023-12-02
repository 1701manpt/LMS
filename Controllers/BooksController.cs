using LMS.Models;
using LMS.Services.Interfaces;
using LMS.ViewModels.Books;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Books
        public IActionResult Index(int? pageNumber, int? pageSize)
        {
            try
            {
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
                    TotalPages = _bookService.CountPage((int)pageSize),
                    CurrentPage = (int)pageNumber,
                    PageSize = (int)pageSize
                };

                IndexViewModel indexViewModel = new IndexViewModel
                {
                    Books = _bookService.GetByPage((int)pageNumber, (int)pageSize),
                    PaginationPartialViewModel = pagination
                };

                return View(indexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_bookService.Exist((int)id))
                {
                    return NotFound();
                }

                var book = _bookService.Details((int)id);

                return View(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Books/Create
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

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NumberOfPages,Id,Type,Title,Author,PublicationDate,Price,Quantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.Create(book);

                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_bookService.Exist((int)id))
                {
                    return NotFound();
                }

                var book = _bookService.Details((int)id);

                return View(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("NumberOfPages,Id,Type,Title,Author,PublicationDate,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bookService.Edit(book);

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_bookService.Exist((int)id))
                {
                    return NotFound();
                }

                var book = _bookService.Details((int)id);

                return View(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _bookService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

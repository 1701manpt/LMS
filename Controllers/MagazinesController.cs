using LMS.Models;
using LMS.Services;
using LMS.Services.Interfaces;
using LMS.ViewModels.Magazines;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace LMS.Controllers
{
    public class MagazinesController : Controller
    {
        private readonly IMagazineService _magazineService;

        public MagazinesController(IMagazineService magazineService)
        {
            _magazineService = magazineService;
        }

        // GET: Magazines
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
                    TotalPages = _magazineService.CountPage((int)pageSize),
                    CurrentPage = (int)pageNumber,
                    PageSize = (int)pageSize
                };

                IndexViewModel indexViewModel = new IndexViewModel
                {
                    Magazines = _magazineService.GetByPage((int)pageNumber, (int)pageSize),
                    PaginationPartialViewModel = pagination
                };

                return View(indexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Magazines/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_magazineService.Exist((int)id))
                {
                    return NotFound();
                }

                var magazine = _magazineService.Details((int)id);

                return View(magazine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Magazines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Type,Title,Author,PublicationDate,Price,Quantity")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                _magazineService.Create(magazine);

                return RedirectToAction(nameof(Index));
            }
            return View(magazine);
        }

        // GET: Magazines/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_magazineService.Exist((int)id))
                {
                    return NotFound();
                }

                var magazine = _magazineService.Details((int)id);

                return View(magazine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Magazines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Type,Title,Author,PublicationDate,Price")] Magazine magazine)
        {
            try
            {
                if (id != magazine.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _magazineService.Edit(magazine);

                    return RedirectToAction(nameof(Index));
                }
                return View(magazine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Magazines/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_magazineService.Exist((int)id))
                {
                    return NotFound();
                }

                var magazine = _magazineService.Details((int)id);

                return View(magazine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Magazines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _magazineService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

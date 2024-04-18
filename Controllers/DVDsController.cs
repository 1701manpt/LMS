using LMS.Models;
using LMS.Services;
using LMS.Services.Interfaces;
using LMS.ViewModels.Dvds;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LMS.Controllers
{
    public class DvdsController : Controller
    {
        private readonly IDvdService _dvdService;

        public DvdsController(IDvdService dvdService)
        {
            _dvdService = dvdService;
        }

        // GET: Dvds
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

                var itemList = _dvdService.Index();

                PaginationPartialViewModel pagination = new()
                {
                    TotalPages = _dvdService.CountPage(itemList, (int)pageSize),
                    CurrentPage = (int)pageNumber,
                    PageSize = (int)pageSize
                };

                IndexViewModel indexViewModel = new()
                {
                    Dvds = _dvdService
                                .GetPageByNumberPage(itemList, (int)pageNumber, (int)pageSize)
                                .ToList(),
                    PaginationPartialViewModel = pagination
                };

                return View(indexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Dvds/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_dvdService.Exist((int)id))
                {
                    return NotFound();
                }

                var item = _dvdService.Details((int)id);

                return View(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Dvds/Create
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

        // POST: Dvds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RunTime,Id,Type,Title,Author,PublicationDate,Price,Quantity")] Dvd dvd)
        {
            if (ModelState.IsValid)
            {
                _dvdService.Create(dvd);

                return RedirectToAction(nameof(Index));
            }
            return View(dvd);
        }

        // GET: Dvds/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_dvdService.Exist((int)id))
                {
                    return NotFound();
                }

                var dvd = _dvdService.Details((int)id);

                return View(dvd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Dvds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RunTime,Id,Type,Title,Author,PublicationDate,Price")] Dvd dvd)
        {
            if (id != dvd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dvdService.Edit(dvd);

                return RedirectToAction(nameof(Index));
            }

            return View(dvd);
        }

        // GET: Dvds/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_dvdService.Exist((int)id))
                {
                    return NotFound();
                }

                var dvd = _dvdService.Details((int)id);

                return View(dvd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Dvds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _dvdService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

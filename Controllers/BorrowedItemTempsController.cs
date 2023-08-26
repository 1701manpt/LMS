using LMS.Models;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.Controllers
{
    public class BorrowedItemTempsController : Controller
    {
        private readonly IBorrowedItemTempService _borrowedItemTempService;
        private readonly IItemService _itemService;

        public BorrowedItemTempsController(IBorrowedItemTempService borrowedItemTempService, IItemService itemService)
        {
            _borrowedItemTempService = borrowedItemTempService;
            _itemService = itemService;
        }

        // GET: BorrowedItemTemps/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var borrowedItemTemp = _borrowedItemTempService.Details((int)id);
                if (borrowedItemTemp == null)
                {
                    return NotFound();
                }

                return View(borrowedItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: BorrowedItemTemps/Create
        public IActionResult Create()
        {
            try
            {
                var itemSelectList = _itemService.Index()
                    .Select(b => new
                    {
                        Value = b.Id,
                        Text = $"{b.Title} - {b.Type}"
                    });

                ViewData["ItemId"] = new SelectList(itemSelectList, "Value", "Text");
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: BorrowedItemTemps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ItemId,Quantity")] BorrowedItemTemp borrowedItemTemp)
        {
            try
            {
                var itemSelectList = _itemService.Index()
                .Select(b => new
                {
                    Value = b.Id,
                    Text = $"{b.Title} - {b.Type}"
                });
                ViewData["ItemId"] = new SelectList(itemSelectList, "Value", "Text");

                if (ModelState.IsValid)
                {
                    _borrowedItemTempService.Create(borrowedItemTemp);

                    return RedirectToAction("Create", "BorrowedHistories");
                }
                return View(borrowedItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: BorrowedItemTemps/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowedItemTempService.Exist((int)id))
                {
                    return NotFound();
                }

                var borrowedItemTemp = _borrowedItemTempService.Details((int)id);

                ViewData["ItemId"] = new SelectList(_itemService.Index(), "Id", "Title", borrowedItemTemp.ItemId);

                return View(borrowedItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: BorrowedItemTemps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ItemId,Quantity,Cost")] BorrowedItemTemp borrowedItemTemp)
        {
            try
            {
                if (id != borrowedItemTemp.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _borrowedItemTempService.Edit(borrowedItemTemp);

                    return RedirectToAction("Create", "BorrowedHistories");
                }
                return View(borrowedItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: BorrowedItemTemps/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_borrowedItemTempService.Exist((int)id))
                {
                    return NotFound();
                }

                var borrowedItemTemp = _borrowedItemTempService.Details((int)id);

                return View(borrowedItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: BorrowedItemTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _borrowedItemTempService.Delete(id);

                return RedirectToAction("Create", "BorrowedHistories");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

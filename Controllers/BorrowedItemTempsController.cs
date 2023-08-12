using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class BorrowedItemTempsController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowedItemTempsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BorrowedItemTemps
        public async Task<IActionResult> Index()
        {
            return _context.BorrowedItemTemps != null ?
                        View(await _context.BorrowedItemTemps.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.BorrowedItemTemp'  is null.");
        }

        // GET: BorrowedItemTemps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BorrowedItemTemps == null)
            {
                return NotFound();
            }

            var borrowedItemTemp = await _context.BorrowedItemTemps
                .Include(bit => bit.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedItemTemp == null)
            {
                return NotFound();
            }

            return View(borrowedItemTemp);
        }

        // GET: BorrowedItemTemps/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Title");
            return View();
        }

        // POST: BorrowedItemTemps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemId,Quantity,Cost")] BorrowedItemTemp borrowedItemTemp)
        {
            borrowedItemTemp.Cost = borrowedItemTemp.Quantity * _context.Items.Find(borrowedItemTemp.ItemId).Price;
            if (ModelState.IsValid)
            {
                _context.Add(borrowedItemTemp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "BorrowedHistories");
            }
            return View(borrowedItemTemp);
        }

        // GET: BorrowedItemTemps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BorrowedItemTemps == null)
            {
                return NotFound();
            }

            var borrowedItemTemp = await _context.BorrowedItemTemps.FindAsync(id);
            if (borrowedItemTemp == null)
            {
                return NotFound();
            }

            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Title", borrowedItemTemp.ItemId);

            return View(borrowedItemTemp);
        }

        // POST: BorrowedItemTemps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemId,Quantity,Cost")] BorrowedItemTemp borrowedItemTemp)
        {
            if (id != borrowedItemTemp.Id)
            {
                return NotFound();
            }

            borrowedItemTemp.Cost = borrowedItemTemp.Quantity * _context.Items.Find(borrowedItemTemp.ItemId).Price;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedItemTemp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedItemTempExists(borrowedItemTemp.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { Id = borrowedItemTemp.Id });
            }
            return View(borrowedItemTemp);
        }

        // GET: BorrowedItemTemps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BorrowedItemTemps == null)
            {
                return NotFound();
            }

            var borrowedItemTemp = await _context.BorrowedItemTemps
                .Include(bit => bit.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedItemTemp == null)
            {
                return NotFound();
            }

            return View(borrowedItemTemp);
        }

        // POST: BorrowedItemTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BorrowedItemTemps == null)
            {
                return Problem("Entity set 'AppDbContext.BorrowedItemTemp'  is null.");
            }
            var borrowedItemTemp = await _context.BorrowedItemTemps.FindAsync(id);
            if (borrowedItemTemp != null)
            {
                _context.BorrowedItemTemps.Remove(borrowedItemTemp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "BorrowedHistories");
        }

        private bool BorrowedItemTempExists(int id)
        {
            return (_context.BorrowedItemTemps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

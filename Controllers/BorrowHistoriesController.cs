using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BorrowedHistories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.BorrowedHistories
                .Include(b => b.Borrower)
                .Include(b => b.BorrowedItems);
            return View(await appDbContext.ToListAsync());
        }

        // GET: BorrowedHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BorrowedHistories == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowedHistories
                .Include(_ => _.Borrower)
                .Include(_ => _.BorrowedItems)
                .ThenInclude(_ => _.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            return View(borrowHistory);
        }

        // GET: BorrowedHistories/Create
        public IActionResult Create()
        {
            var borrowerList = _context.Borrowers
               .Select(b => new
               {
                   Value = b.Id.ToString(),
                   Text = $"{b.CardNumber} - {b.Name}"
               });
            ViewBag.BorrowerId = (IEnumerable<SelectListItem>)new SelectList(borrowerList, "Value", "Text");
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy");
            ViewData["BorrowedDate"] = formattedDate;

            var borrowedItemTempList = _context.BorrowedItemTemps.ToList();

            ViewData["BorrowedItems"] = borrowedItemTempList;

            decimal totalCost = 0;
            foreach (var item in borrowedItemTempList)
            {
                totalCost += (decimal)item.Cost;
            }

            ViewData["TotalCost"] = totalCost;

            return View();
        }

        // POST: BorrowedHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowerId,BorrowedDate,TotalCost")] BorrowedHistory borrowHistory)
        {
            borrowHistory.BorrowedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _context.BorrowedHistories.AddAsync(borrowHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "BorrowedItems", new { id = borrowHistory.Id });
            }
            return View();
        }

        // GET: BorrowedHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BorrowedHistories == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowedHistories
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            return View(borrowHistory);
        }

        // POST: BorrowedHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BorrowedHistories == null)
            {
                return Problem("Entity set 'AppDbContext.BorrowedHistories'  is null.");
            }
            var borrowHistory = await _context.BorrowedHistories.FindAsync(id);
            if (borrowHistory != null)
            {
                _context.BorrowedHistories.Remove(borrowHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowHistoryExists(int id)
        {
            return (_context.BorrowedHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace LMS.Controllers
{
    public class BorrowedHistoriesController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowedHistoriesController(AppDbContext context)
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

        [HttpPost]
        public IActionResult SearchBorrowers()
        {
            return Redirect("Create");
        }

        // GET: BorrowedHistories/Create
        public IActionResult Create()
        {
            var borrowerSelectList = _context.Borrowers
               .Select(b => new
               {
                   Value = b.Id,
                   Text = $"{b.CardNumber} - {b.Name}"
               });
            ViewData["BorrowerId"] = new SelectList(borrowerSelectList, "Value", "Text");
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            ViewData["BorrowedDate"] = currentDate;

            var borrowedItemTempList = _context.BorrowedItemTemps.Include(bit => bit.Item);

            ViewData["BorrowedItemTemps"] = borrowedItemTempList;

            decimal totalCost = _context.BorrowedItemTemps
                .Include(bit => bit.Item)
                .Sum(bit => bit.Quantity * bit.Item.Price);

            ViewData["TotalCost"] = totalCost.ToString("F0");

            return View();
        }

        // POST: BorrowedHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowerId,BorrowedDate,TotalCost")] BorrowedHistory borrowHistory)
        {
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(_ => _.Id == borrowHistory.BorrowerId);
            if (borrower == null)
            {
                return NotFound();
            }
            borrowHistory.BorrowedDate = DateTime.Now;
            borrowHistory.TotalCost = _context.BorrowedItemTemps
                .Include(bit => bit.Item)
                .Sum(bit => bit.Quantity * bit.Item.Price);
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

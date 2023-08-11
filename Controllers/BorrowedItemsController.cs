using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class BorrowedItemsController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowedItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BorrowedItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.BorrowedItems.Include(b => b.BorrowedHistory).Include(b => b.Item);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Create(int id)
        {
            var borrowHistoryId = id;
            var borrowedItemTempList = await _context.BorrowedItemTemps.ToListAsync();

            foreach (var item in borrowedItemTempList)
            {
                var borrowedItem = new BorrowedItem
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    Cost = item.Cost,
                    BorrowHistoryId = borrowHistoryId
                };
                await _context.BorrowedItems.AddAsync(borrowedItem);
                _context.BorrowedItemTemps.Remove(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "BorrowedHistories");
        }
    }
}

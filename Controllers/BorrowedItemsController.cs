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

        public async Task<IActionResult> Create(int id)
        {
            var borrowHistoryId = id;
            var borrowedItemTempList = await _context.BorrowedItemTemps
                .Include(bit => bit.Item)
                .ToListAsync();

            foreach (var bit in borrowedItemTempList)
            {
                var borrowedItem = new BorrowedItem
                {
                    ItemId = bit.ItemId,
                    Quantity = bit.Quantity,
                    Cost = bit.Quantity * bit.Item.Price,
                    BorrowHistoryId = borrowHistoryId
                };
                await _context.BorrowedItems.AddAsync(borrowedItem);
                _context.BorrowedItemTemps.Remove(bit);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "BorrowedHistories");
        }
    }
}

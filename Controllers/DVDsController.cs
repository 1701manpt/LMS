using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class DVDsController : Controller
    {
        private readonly AppDbContext _context;

        public DVDsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DVDs
        public async Task<IActionResult> Index()
        {
            return _context.DVDs != null ?
                        View(await _context.DVDs.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.DVDs'  is null.");
        }

        // GET: DVDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DVDs == null)
            {
                return NotFound();
            }

            var dVD = await _context.DVDs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dVD == null)
            {
                return NotFound();
            }

            return View(dVD);
        }

        // GET: DVDs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DVDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RunTime,Id,Type,Title,Author,PublicationDate,Price")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dVD);
        }

        // GET: DVDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DVDs == null)
            {
                return NotFound();
            }

            var dVD = await _context.DVDs.FindAsync(id);
            if (dVD == null)
            {
                return NotFound();
            }
            return View(dVD);
        }

        // POST: DVDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RunTime,Id,Type,Title,Author,PublicationDate,Price")] DVD dVD)
        {
            if (id != dVD.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDExists(dVD.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dVD);
        }

        // GET: DVDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DVDs == null)
            {
                return NotFound();
            }

            var dVD = await _context.DVDs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dVD == null)
            {
                return NotFound();
            }

            return View(dVD);
        }

        // POST: DVDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DVDs == null)
            {
                return Problem("Entity set 'AppDbContext.DVDs'  is null.");
            }
            var dVD = await _context.DVDs.FindAsync(id);
            if (dVD != null)
            {
                _context.DVDs.Remove(dVD);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDExists(int id)
        {
            return (_context.DVDs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

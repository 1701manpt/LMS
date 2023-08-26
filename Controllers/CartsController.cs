using LMS.Models;
using LMS.Services.Interfaces;
using LMS.ViewModels.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemTempService _cartItemTempService;

        public CartsController(ICartService cartService, ICartItemTempService cartItemTempService)
        {
            _cartService = cartService;
            _cartItemTempService = cartItemTempService;
        }

        // GET: Carts
        public IActionResult Index()
        {


            return View();
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            return View();
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            IndexViewModel indexViewModel = new IndexViewModel
            {
                BorrowedDate = DateTime.Now,
                TotalCost = _cartService.CalcTotalCost(),
                CartItemTempList = _cartItemTempService.Index()
            };

            return View(indexViewModel);
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowerId,BorrowedDate,TotalCost")] Cart cart)
        {
            if (ModelState.IsValid)
            {
            }

            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            return View();
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}

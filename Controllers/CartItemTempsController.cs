using LMS.Models;
using LMS.Services;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class CartItemTempsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICartItemTempService _cartItemTempService;
        private readonly IItemService _itemService;

        public CartItemTempsController(ICartItemTempService cartItemTempService, AppDbContext context, IItemService itemService)
        {
            _cartItemTempService = cartItemTempService;
            _context = context;
            _itemService = itemService;
        }

        // GET: CartItemTemps/Create
        public IActionResult Create(int? itemId)
        {
            try
            {
                if (itemId == null)
                {
                    return NotFound();
                }

                ViewData["ItemId"] = (int)itemId;
                ViewData["Item"] = _itemService.Details((int)itemId).Type + " - " + _itemService.Details((int)itemId).Title;

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CartItemTemps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int? itemId, [Bind("Id,ItemId,Quantity,Cost")] CartItemTemp cartItemTemp)
        {
            try
            {
                if (itemId == null)
                {
                    return BadRequest();
                }

                if (itemId != cartItemTemp.ItemId)
                {
                    return BadRequest();
                }

                cartItemTemp.BorrowerId = 1;

                if (ModelState.IsValid)
                {
                    _cartItemTempService.Create(cartItemTemp);

                    return RedirectToAction("Create", "Carts");
                }

                ViewData["ItemId"] = (int)itemId;
                ViewData["Item"] = _itemService.Details((int)itemId).Type + " - " + _itemService.Details((int)itemId).Title;

                return View(cartItemTemp);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: CartItemTemps/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_cartItemTempService.Exist((int)id))
                {
                    return NotFound();
                }

                var cartItemTemp = _cartItemTempService.Details((int)id);

                return View(cartItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CartItemTemps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ItemId,Quantity,Cost")] CartItemTemp cartItemTemp)
        {
            try
            {
                if (id != cartItemTemp.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _cartItemTempService.Edit(cartItemTemp);

                    return RedirectToAction("Create", "Carts");
                }
                return View(cartItemTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: CartItemTemps/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_cartItemTempService.Exist((int)id))
                {
                    return NotFound();
                }

                var cartItemTemp = _cartItemTempService.Details((int)id);

                return View(cartItemTemp);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CartItemTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _cartItemTempService.Delete(id);

                return RedirectToAction("Create", "Carts");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

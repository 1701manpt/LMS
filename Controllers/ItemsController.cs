using LMS.Models;
using LMS.Services;
using LMS.Services.Interfaces;
using LMS.ViewModels.Items;
using LMS.Views.Shared.PartialViews;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: Items
        public IActionResult Index(int? pageNumber, int? pageSize, string? title)
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

                var itemList = _itemService.Index();

                PaginationPartialViewModel pagination = new()
                {
                    TotalPages = _itemService.CountPage(itemList, (int)pageSize),
                    CurrentPage = (int)pageNumber,
                    PageSize = (int)pageSize
                };

                IndexViewModel indexViewModel = new()
                {
                    Items = _itemService
                                .GetPageByNumberPage(itemList, (int)pageNumber, (int)pageSize)
                                .ToList(),
                    PaginationPartialViewModel = pagination,
                    Title = title
                };

                return View(indexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Search(string? title)
        {
            return RedirectToAction("Index", new { title });
        }

        // GET: Items/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_itemService.Exist((int)id))
                {
                    return NotFound();
                }

                var item = _itemService.Details((int)id);

                return View(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Create(int? itemType)
        {
            try
            {
                ItemType[] arr = (ItemType[])Enum.GetValues(typeof(ItemType));
                ViewData["ItemTypes"] = arr;
                ViewData["ItemType"] = itemType;
                
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_itemService.Exist((int)id))
                {
                    return NotFound();
                }

                var item = _itemService.Details((int)id);

                return View(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Type,Title,Author,PublicationDate,Price")] Item item)
        {
            try
            {
                if (id != item.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _itemService.Edit(item);

                    return RedirectToAction(nameof(Index));
                }
                return View(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Items/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!_itemService.Exist((int)id))
                {
                    return NotFound();
                }

                var item = _itemService.Details((int)id);

                return View(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _itemService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

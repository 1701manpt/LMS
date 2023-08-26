using LMS.Models;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;

        public HomeController(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        public IActionResult Index()
        {
            return View(_itemService.Index());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
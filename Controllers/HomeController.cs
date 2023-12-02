using LMS.Models;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;
        private readonly SessionManager _sessionManager;

        public HomeController(IItemService itemService, SessionManager sessionManager)
        {
            _itemService = itemService;
            _sessionManager = sessionManager;
        }
        
        public IActionResult Index()
        {
            if(!_sessionManager.IsExist("myListKey"))
            {
                _sessionManager.SetSessionData("myListKey", new List<string>());
            }

            List<string> retrievedList = _sessionManager.GetSessionData<string>("myListKey");

            ViewData["data"] = retrievedList;

            return View(_itemService.Index());
        }

        public IActionResult Add()
        {
            // Lấy danh sách từ Session
            List<string> retrievedList = _sessionManager.GetSessionData<string>("myListKey");

            retrievedList.Add("123");

            _sessionManager.SetSessionData("myListKey", retrievedList);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
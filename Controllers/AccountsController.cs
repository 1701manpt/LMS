using LMS.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            LogInViewModel viewModel = new LogInViewModel();
            return View(viewModel);
        }
    }
}

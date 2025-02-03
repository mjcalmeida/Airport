using System.Diagnostics;
using Airport.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

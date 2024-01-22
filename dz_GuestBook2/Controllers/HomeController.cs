using dz_GuestBook2.Models;
using dz_GuestBook2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace dz_GuestBook2.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IMesRepository _rep;

        public HomeController(IMesRepository rep)
        {
            _rep=rep;
        }

        public async Task<IActionResult> Index()
        {
            //if (HttpContext.Session.GetString("Name") != null)
            //    return View();
            //else
            //    return RedirectToAction("Login", "Account");

            var rep1 = _rep.GetAllMessages();
            
            
            return View(await rep1);


            //return _repository.GetAllMessages();
            //return View(_repository.GetAllMessages());
        }

        public ActionResult Logout()
        {
            // очищается сессия
            HttpContext.Session.Clear();
            // переадресация на Login на контроллере Account
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

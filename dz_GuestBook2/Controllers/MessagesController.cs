using Microsoft.AspNetCore.Mvc;
using dz_GuestBook2.Repository;
using dz_GuestBook2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dz_GuestBook2.Controllers
{
    public class MessagesController : Controller
    {
        IMesRepository _repository;
        public MessagesController(IMesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
           return View(await _repository.GetAllMessages());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Messages mes)
        {
            string log = HttpContext.Session.GetString("Login");
            if (ModelState.IsValid)
            {
                await _repository.CreateMessage(mes, log);
                await _repository.SaveChanges();
                return Redirect("/Home/Index");
            }           
            return View(mes);
        }
    }
}

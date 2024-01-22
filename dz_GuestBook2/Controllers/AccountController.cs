using Microsoft.AspNetCore.Mvc;
using dz_GuestBook2.Repository;
using dz_GuestBook2.Models;

namespace dz_GuestBook2.Controllers
{
    public class AccountController : Controller
    {
        IAccRepository _repository;
        public AccountController(IAccRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if ((await _repository.GetAllUsers()).Count == 0)
                return RedirectToAction("Register", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if ((await _repository.GetAllUsers()).Count == 0)
                return RedirectToAction("Register", "Account");
            if (!ModelState.IsValid)
                return View(model);

            IQueryable<User> users = _repository.GetUsersByLogin(model);

            if (!users.Any())
            {
                ModelState.AddModelError("", "Incorrect login or password!");
                return View(model);
            }

            User user = users.First();

           
            HttpContext.Session.SetString("Name", user.Name ?? string.Empty);
            HttpContext.Session.SetString("Login", user.Login);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (await _repository.IsLoginExists(model.Login))
            {
                ModelState.AddModelError("", "This login is taken!");
                return View(model);
            }

            User user = new()
            {
                Name = model.Name,
                Login = model.Login
            };
            user = await _repository.CreatePassword(user, model);

            await _repository.AddUserToDb(user);

            return RedirectToAction("Login");
        }


    }
}

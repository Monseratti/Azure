using HW0703.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HW0703.Controllers
{
    public class LogRegController : Controller
    {
        ImageContext _context;

        public LogRegController(ImageContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
			var user = await _context.Users.Where(u=>u.Email.Equals(email)&&u.Password.Equals(password)).FirstAsync();
			if (user == null) return RedirectToAction("Login");
			if (user.IsBlocked) return RedirectToAction("Warning");
			var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
				};
			var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			await HttpContext.SignInAsync(claimsPrincipal);
			return RedirectToAction("Index", "Home");
        }

		public async Task<IActionResult> Registration()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Registration([Bind] User user)
		{
            user.Id = (await _context.Users.ToListAsync()).Count == 0 ? 0 : (await _context.Users.ToListAsync()).Last().Id + 1;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

        public async Task<IActionResult> Logout()
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}

		public async Task<IActionResult> Warning()
		{
			return View();
		}
	}
}

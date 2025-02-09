using E_Commarce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;

namespace E_Commarce_Website.Controllers
{
    public class RegisterController : Controller
    {
        private readonly myContext _context;

        public RegisterController(myContext context)
        {
            _context = context;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.tbl_Register.FirstOrDefaultAsync(u => u.cust_email == model.cust_email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email already exists! Please use a different email.");
                    return View(model);
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.cust_Password);

                var account = new Registration
                {
                    cust_name = model.cust_name,
                    cust_phone = model.cust_phone,
                    cust_email = model.cust_email,
                    cust_Password = hashedPassword,
                    cust_country = model.cust_country,
                    cust_city = model.cust_city,
                    cust_address = model.cust_address,
                    cust_gender = model.cust_gender,
                };

                try
                {
                    _context.tbl_Register.Add(account);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"{account.cust_name} registered successfully!";
                    return RedirectToAction("Login");
                }
                catch
                {
                    ModelState.AddModelError("", "Error saving data. Please try again.");
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.tbl_Register.FirstOrDefaultAsync(u => u.cust_email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.cust_Password))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.cust_name),
            new Claim(ClaimTypes.Email, user.cust_email)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Ensure authentication scheme is correct
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Customer");
            }

            ViewBag.ErrorMessage = "Invalid email or password!";
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index","Customer");
        }
    }
}

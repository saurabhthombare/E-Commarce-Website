using E_Commarce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commarce_Website.Controllers
{
    public class RegisterController : Controller
    {
        private readonly myContext _context;

        public RegisterController(myContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                // Ensure that _context.tbl_Register is correctly named in DbContext
                var account = new Registration
                {
                    cust_name = model.cust_name,
                    cust_phone = model.cust_phone,
                    cust_email = model.cust_email,
                    cust_Password = model.cust_Password,
                    cust_country = model.cust_country,
                    cust_city = model.cust_city,
                    cust_address = model.cust_address,
                    cust_gender = model.cust_gender,
                    cust_image = model.cust_image
                };

                try
                {
                    _context.tbl_Register.Add(account); // Check if this matches your DbContext
                    _context.SaveChanges(); // Save to the database

                    TempData["SuccessMessage"] = $"{account.cust_name} registered successfully!";

                    return RedirectToAction("Index"); // Redirect after successful registration
                }
                catch (DbUpdateException ex)
                {
                    // Log the error for debugging
                    Console.WriteLine($"Database Error: {ex.Message}");

                    ModelState.AddModelError("", "Please enter a unique Email or Password.");
                    return View(model);
                }
            }

            return RedirectToAction("Login","Admin");
        }
    }
}

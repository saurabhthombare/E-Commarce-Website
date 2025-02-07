using E_Commarce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commarce_Website.Controllers
{
    public class AdminController : Controller
    {
        private myContext _context;
        private IWebHostEnvironment _env;

        public Product? product { get; private set; }

        public AdminController(myContext context,IWebHostEnvironment env) 
        { 
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            // string admin_session=HttpContext.Session.GetString("admin_session");
            //if(admin_session!=null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View();
            
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string adminEmail,string adminPassword)
        {
           var row= _context.tbl_admin.FirstOrDefault(a=>a.admin_email == adminEmail);
            if (row! == null && row.admin_password == adminPassword)
            
            {
               HttpContext.Session.SetString("admin_session",row.admin_id.ToString());
                return RedirectToAction(   "Admin","index");
            }
            else
            {
                return View();

            }

            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("Login");
        }
        public IActionResult fetchCategory()
        {
            return View(_context.tbl_category.ToList());
        }

        [HttpGet]
        public IActionResult addCategory()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult addCategory(Category cat)
        {
               _context.tbl_category.Add(cat);
                _context.SaveChanges();
                return RedirectToAction("fetchCategory");
            
        
        }

        [HttpGet]
        public IActionResult updateCategory(int id)
        {
            var category = _context.tbl_category.Find(id);
             return View(category);
        }

        [HttpPost]
        public IActionResult updateCategory(Category cat)
        {
           
                _context.tbl_category.Update(cat);
                _context.SaveChanges();
                return RedirectToAction("fetchCategory"); 
            }
         

        public IActionResult deletePermissionCategory(int id)
        {
            return View( _context.tbl_category.FirstOrDefault(c => c.category_id == id));
            
        }

        public IActionResult deleteCategory(int id)
        {
            var category = _context.tbl_category.Find(id);
            _context.tbl_category.Remove(category);
           _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult fetchProduct()
        {
            return View(_context.tbl_product.ToList());
        }
        public IActionResult addProduct()
        {
            List<Category> categories=_context.tbl_category.ToList();
            ViewData["category"]=categories;
            return View();
        }
        [HttpPost]
        public IActionResult addProduct(Product prod, IFormFile product_image)
        {
            string imageName = Path.GetFileName(product_image.FileName);
            string imagePath = Path.Combine(_env.WebRootPath,"product_image",imageName);
            FileStream fs=new FileStream(imagePath,FileMode.Create);
            product_image.CopyTo(fs);
            prod.product_image = imageName;
            _context.tbl_product.Add(prod);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult ProductDetails(int id)
        {
            //return View(_context.tbl_product.FirstOrDefault(p => p.product_id == id));
            return View(_context.tbl_product.Include(p=>p.Category).FirstOrDefault(p=>p.product_id==id));

        }
        public IActionResult deletePermissionProduct(int id)
        {
            return View(_context.tbl_product.FirstOrDefault(p => p.product_id == id));

        }
        public IActionResult deleteProduct(int id)
        {
            var product = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        [HttpGet]
        public IActionResult updateProduct(int id)
        {
            List<Category> categories=_context.tbl_category.ToList();
            ViewData["category"]= categories;
           
            var product = _context.tbl_product.Find(id);
            ViewBag.selectedCategoryId = product.cat_id;
            return View(product);
        }

        [HttpPost]
        public IActionResult updateProduct(Product product)
        {

            _context.tbl_product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        [HttpPost]
        public IActionResult ChangeProductImage(IFormFile product_image, Product product)
        {
            if (product_image == null || product_image.Length == 0)
            {
                ModelState.AddModelError("product_image", "Please upload an image.");
                return RedirectToAction("fetchProduct");
            }

            // Validate file type (allow only images)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(product_image.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("product_image", "Invalid file format.");
                return RedirectToAction("fetchProduct");
            }

            // Find the existing product by ID
            var existingProduct = _context.tbl_product.Find(product.product_id);
            if (existingProduct == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            // Generate a unique filename to prevent overwriting
            string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            string uploadPath = Path.Combine(_env.WebRootPath, "product_image");

            // Ensure directory exists
            Directory.CreateDirectory(uploadPath);

            string imagePath = Path.Combine(uploadPath, uniqueFileName);

            // Save file using a FileStream inside a using block
            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                product_image.CopyTo(fs);
            }

            // Delete the old image (optional)
            if (!string.IsNullOrEmpty(existingProduct.product_image))
            {
                string oldImagePath = Path.Combine(uploadPath, existingProduct.product_image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            // Update product image
            existingProduct.product_image = uniqueFileName;
            _context.SaveChanges();

            return RedirectToAction("fetchProduct");
        }


    }

}

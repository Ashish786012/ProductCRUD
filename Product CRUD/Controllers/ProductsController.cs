// =============================================
// File: Controllers/ProductsController.cs
// Framework: ASP.NET Core MVC
// Key differences from MVC 5:
//   - using Microsoft.AspNetCore.Mvc  (not System.Web.Mvc)
//   - NotFound() instead of HttpNotFound()
//   - IActionResult instead of ActionResult
//   - No [ValidateAntiForgeryToken] needed on GET actions
// =============================================

using Microsoft.AspNetCore.Mvc;
using ProductCRUD.Models;
using ProductCRUD.Repository;

namespace ProductCRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _repo;

        public ProductsController(ProductRepository repo)
        {
            _repo = repo;
        }

        // -----------------------------------------------
        // INDEX — GET /Products
        // -----------------------------------------------
        public IActionResult Index()
        {
            var products = _repo.GetAll();
            return View(products);
        }

        // -----------------------------------------------
        // DETAILS — GET /Products/Details/5
        // -----------------------------------------------
        public IActionResult Details(int id)
        {
            var product = _repo.GetById(id);

            if (product == null)
                return NotFound(); // ✅ Core uses NotFound(), not HttpNotFound()

            return View(product);
        }

        // -----------------------------------------------
        // CREATE — GET /Products/Create
        // -----------------------------------------------
        public IActionResult Create()
        {
            return View();
        }

        // -----------------------------------------------
        // CREATE — POST /Products/Create
        // -----------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.Create(product);
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index)); // nameof() is preferred in Core
            }

            return View(product);
        }

        // -----------------------------------------------
        // EDIT — GET /Products/Edit/5
        // -----------------------------------------------
        public IActionResult Edit(int id)
        {
            var product = _repo.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // -----------------------------------------------
        // EDIT — POST /Products/Edit/5
        // -----------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(product);
                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // -----------------------------------------------
        // DELETE — GET /Products/Delete/5
        // -----------------------------------------------
        public IActionResult Delete(int id)
        {
            var product = _repo.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // -----------------------------------------------
        // DELETE — POST /Products/Delete/5
        // -----------------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Product deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Search(string query)
        //{
        //    var products = _repo.Search(query);
        //    return View("Index", products);
        //}
    }
}

using Microsoft.AspNetCore.Mvc;
using NovelNest.DataAccess;
using NovelNest.Models;

namespace NovelNest.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        { 
            _db = db; 
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); ;
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {


            if(obj.DisplayOrder < 0 || obj.DisplayOrder > 100)
            {
                ModelState.AddModelError("displayOrder", "DisplayOrder must be between 1 - 100");
            }

             if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder cannot match the Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category changed successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Category? obj = _db.Categories.Find(id);

            if(obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}

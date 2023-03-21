using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using UdemyCourse.Data;
using UdemyCourse.Models;

namespace UdemyCourse.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbConntext _db;

        public CategoryController(ApplicationDbConntext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Category;
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}

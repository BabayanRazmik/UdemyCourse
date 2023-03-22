using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyCourse.Data;
using UdemyCourse.Models;

namespace UdemyCourse.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbConntext _db;

        public ApplicationTypeController(ApplicationDbConntext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _db.ApplicationType;
            return View(objList);
        }

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationType model)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationType.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index", "ApplicationType");
            }
            return View(model);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ApplicationType.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType app)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationType.Update(app);
                _db.SaveChanges();
                return RedirectToAction("Index", "ApplicationType");
            }
            return View(app);
        }

        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ApplicationType.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ApplicationType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "ApplicationType");
        }

        #endregion
    }
}

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationType model)
        {
            _db.ApplicationType.Add(model);
            _db.SaveChanges();
            return View();
        }
    }
}

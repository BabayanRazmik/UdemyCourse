using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UdemyCourse.Data;
using UdemyCourse.Models;
using UdemyCourse.Models.ViewModels;
using UdemyCourse.Utility;

namespace UdemyCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbConntext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbConntext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product.Include(p => p.Category).Include(p => p.ApplicationType),
                Categories = _db.Category
            };
            return View(homeVM);
        }

        #region Details

        [HttpGet]
        public IActionResult Details(int id) 
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }


            DetailsVM DetailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(p => p.Category).Include(p => p.ApplicationType)
                .Where(p => p.Id == id).FirstOrDefault(),
                ExistsInCardt = false

            };

            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == id)
                {
                    DetailsVM.ExistsInCardt = true;
                }
            }

            return View(DetailsVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            shoppingCartList.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var itemToRemove = shoppingCartList.FirstOrDefault(sh => sh.ProductId == id);

            if (itemToRemove != null)
            { 
                shoppingCartList.Remove(itemToRemove);
            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

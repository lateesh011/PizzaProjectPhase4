using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaProject.Models;
using Microsoft.AspNetCore.Http;

namespace PizzaProject.Controllers
{
    public class MyPizzaController : Controller
    {
        static List<Check> check = new List<Check>();
        List<Pizza> pizza;
        public MyPizzaController()
        {

            pizzadetails a = new pizzadetails();
            a.add();
            pizza = a.pizza;

        }

        public IActionResult Index()
        {

            return View(pizza);
        }

        public IActionResult Addtocart(int id)
        {
            var found = pizza.Find(a => a.PizzaId == id);
            TempData["pid"] = id;

            return View(found);
        }

        [HttpPost]
        public IActionResult Addtocart(IFormCollection collection)

        {


            Check c = new Check();

            int id = Convert.ToInt32(TempData["pid"]);
            c.Quantity = Convert.ToInt32(Request.Form["qnty"]);
            var result = pizza.Find(a => a.PizzaId == id);
            c.ProductId = result.PizzaId;
            c.ProductName = result.PizzaName;
            c.TotalPrice = result.Price * c.Quantity;
            check.Add(c);


            return RedirectToAction("CheckOut");
        }

        public IActionResult CheckOut()
        {
            return View(check);

        }
    }
}

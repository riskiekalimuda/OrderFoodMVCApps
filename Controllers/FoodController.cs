using FoodOrderEntitie.Models;
using FoodOrderEntitie.Utilities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FoodOrderMVC.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Page = pageNumber;
            int pageSize = 5;

            //ViewBag.ListFoods = Food.GetAllFoods().ToPagedList(pageNumber, pageSize); 
            return View();
        }
    }
}

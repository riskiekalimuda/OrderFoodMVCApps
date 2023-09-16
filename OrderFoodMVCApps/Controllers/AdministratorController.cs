using FoodOrderEntitie.Models;
using FoodOrderEntitie.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X.PagedList;

namespace FoodOrderMVC.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index(int? page, string keyword)
        {
            var pageNumber = page ?? 1;
            ViewBag.Page = pageNumber;
            int pageSize = 5;
            List<Administrator> list = new List<Administrator>();
            if (!string.IsNullOrEmpty(keyword))
                list = Administrator.GetAllAdministrator().Where(x => x.AdminName.ToLower().Contains(keyword.ToLower())).ToList();
            else

                list = Administrator.GetAllAdministrator();

            ViewBag.ListAdmin = list.ToPagedList(pageNumber, pageSize);
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(Administrator admin)
        {
            admin.AdminStatus = true;
            admin.AdminCreatedAt = DateTime.Now;
            admin.AdminPassword = EncodeDecodeBase64.EncodeBase64(admin.AdminPassword);
            var obj = Administrator.Insert(admin);
            if (obj != null)
            {
                ViewBag.ErrMessage = obj.ResultMessage;
            }

            return View(admin);
        }
    }
}

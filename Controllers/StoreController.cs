using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class StoreController : Controller
    {

        BookStoreEntities storeDB = new BookStoreEntities();

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id,int? page)
        {
            var book = storeDB.Books.Find(id);

            if (page.HasValue)
                ViewBag.page = page;
            else
            {
                ViewBag.page = 1;
                ViewBag.returnUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            }

            

            return View(book);
        }

    }
}

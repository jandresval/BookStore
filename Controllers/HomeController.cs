using BookStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookStoreEntities storeDB = new BookStoreEntities();


        // Unobtrusive Ajax
        public ActionResult Index(int? page)
        {
            var listPaged = GetPagedNames(page); 
            if (listPaged == null)
                return HttpNotFound();

            ViewBag.Names = listPaged;
            if (page.HasValue)
                ViewBag.page = page;
            else
                ViewBag.page = 1;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("BookListSell")
                : View();
        }

        protected IPagedList<Book> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand
            var listUnpaged = GetStuffFromDatabase();

            // page the list
            const int pageSize = 5;
            var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        
        protected IEnumerable<Book> GetStuffFromDatabase()
        {
            var Book = storeDB.Books;
            return Book.ToList();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

    }
}

using BookStore.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ParameterController : Controller
    {

        private BookStoreEntities db = new BookStoreEntities();

        //
        // GET: /Parameter/

        public ActionResult ListBook()
        {
            var Book = (from r in db.Books select r);

            ViewBag.Message = "Test";

            return View(Book.ToList());

        }

        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {

                db.Books.Add(book);
                db.SaveChanges();
            }

            return RedirectToAction("ListBook", "Parameter");
        }

        public ActionResult EditBook(int Id)
        {
            Book Book = db.Books.Find(Id);
            return View(Book);
        }

        //
        // POST: /Parameter/EditBook/5

        [HttpPost]
        public ActionResult EditBook(int Id, Book books)
        {
            if (ModelState.IsValid)
            {
                var bookItem = db.Books.Single(book => book.Id == Id);
                bookItem.Author = books.Author;
                bookItem.ISBN = books.ISBN;
                bookItem.PathImage = books.PathImage;
                bookItem.Price = books.Price;
                bookItem.Publisher = books.Publisher;
                bookItem.PublishYear = books.PublishYear;
                bookItem.Quantity = books.Quantity;
                bookItem.Summary = books.Summary;
                bookItem.Title = books.Title;
                db.SaveChanges();
                return RedirectToAction("ListBook", "Parameter");
            }
            return View(books);
        }

        //
        // GET: /Parameter/DetailsBook/5

        public ViewResult DetailsBook(int id)
        {
            Book book = db.Books.Find(id);
            return View(book);
        }

        //
        // GET: /Parameter/DeleteBook/5

        public ActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            return View(book);
        }

        //
        // POST: /Parameter/DeleteBook/5

        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book album = db.Books.Find(id);
            db.Books.Remove(album);
            db.SaveChanges();
            return RedirectToAction("ListBook", "Parameter");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}

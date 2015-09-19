using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Administrator,User")]
    public class ReportsController : Controller
    {
        BookStoreEntities storeDB = new BookStoreEntities();

        //
        // GET: /Reports/

        public ActionResult Index()
        {
            /*var orderDetailViewModel = storeDB.OrderDetails.
                Where(r => r.Order.UserName == User.Identity.Name);*/

            var orders = from u in storeDB.Orders
                         where u.UserName == User.Identity.Name
                         select new OrderDetailViewModel
                         {
                             OrderId = u.Id,
                             TotalOrder = u.Total
                         };

            List<OrderDetailViewModel> orderDetailViewModel = orders.ToList();

            foreach (var item in orderDetailViewModel)
            {
                var ordersDetail = from u in storeDB.OrderDetails
                                   where u.OrderId == item.OrderId
                                   select u;

                item.orderDetail = ordersDetail.ToList();

            }

            foreach (var item in orderDetailViewModel)
            {
                foreach (var itemOrder in item.orderDetail)
                {
                    var ordersBooks = (from u in storeDB.Books
                                      where u.Id == itemOrder.BookId
                                      select u).SingleOrDefault();

                    itemOrder.Book = ordersBooks;

                }
            }


            return View(orderDetailViewModel);
        }

    }
}

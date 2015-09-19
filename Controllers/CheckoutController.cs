using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        BookStoreEntities storeDB = new BookStoreEntities();

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            UserProfile userProfile = storeDB.UserProfiles.SingleOrDefault(u => u.UserName == User.Identity.Name);
            Order order = new Order
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Address = userProfile.Adress,
                Town = userProfile.Town,
                Province = userProfile.Province,
                PostalCode = userProfile.PostalCode,
                Country = userProfile.Country,
                PhoneNumber = userProfile.PhoneNumber,
                Email = userProfile.EmailAdress
            };
            return View(order);
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            var cartInfo = ShoppingCart.GetCart(this.HttpContext);
            TryUpdateModel(order);

            try
            {
                    order.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    order.Total = cartInfo.GetTotal();

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.Id });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.Id == id &&
                o.UserName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}

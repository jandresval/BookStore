using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModels
{
    public class OrderDetailViewModel
    {
        public List<OrderDetail> orderDetail { get; set; }

        public int OrderId { get; set; }

        public decimal TotalOrder { get; set; }

    }
}
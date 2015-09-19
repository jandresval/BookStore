using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Models
{
    /// <summary>
    /// Defines the book class to have access at the books table.
    /// </summary>
    [Bind(Exclude = "Id")]
    public class Book
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string PublishYear { get; set; }
        public string Summary { get; set; }
        public string PathImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
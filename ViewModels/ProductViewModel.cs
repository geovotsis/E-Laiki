using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laiki.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laiki.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Thumbnail { get; set; }
        public string Image{ get; set; }
        public string ProducerId { get; set; }
        // Navigation Property
        public ApplicationUser Producer { get; set; }


        public void Modify(string name,string content,decimal price,int quantity,string thumbnail,string image)
        {

            Name = name;
            Content = content;
            Price = price;
            Quantity = quantity;
            Thumbnail = (thumbnail == null ? "/Content/Images/DefaultProductImg.jpg" : image);
            Image = image;

        }

    }
}
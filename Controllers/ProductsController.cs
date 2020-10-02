using Laiki.Models;
using Laiki.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Laiki.Helpers;
using PagedList;

namespace Laiki.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext context;

        public ProductsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Products
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";
            var products = from p in context.Products
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));                                       
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "Quantity":
                    products = products.OrderBy(p => p.Quantity);
                    break;
                case "Quantity_desc":
                    products = products.OrderByDescending(p => p.Quantity);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }
            return View( products.ToList());
        }

        [Authorize]
        public ActionResult MyProducts(string searchString,string sortOrder)
        {
            var userId = User.Identity.GetUserId();
            var products = context.Products
                .Where(p => p.ProducerId == userId);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";
            //var products = from p in context.Products
            //               select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "Quantity":
                    products = products.OrderBy(p => p.Quantity);
                    break;
                case "Quantity_desc":
                    products = products.OrderByDescending(p => p.Quantity);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }
            return View(products.ToList());
        }

        // CREATE: GET
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new ProductViewModel();

            return View(viewModel);
        }

        // CREATE: POST
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            var product = new Product
            {
                ProducerId = User.Identity.GetUserId(),
                Name = viewModel.Name,
                Content = viewModel.Content,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                Thumbnail = "/Content/Images/DefaultProductImg.jpg",
                Image = viewModel.Image
            };



            context.Products.Add(product);
            context.SaveChanges();

            // return RedirectToAction("Edit", "Products"); MOLIS FTIAKSOUME TIN EDIT, REDIRECT EDW
            return RedirectToAction("Index", "Products");
        }

        //Edid: GET
        [Authorize]

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var product = context.Products
                .Single(p => p.Id == id && p.ProducerId == userId);

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Content = product.Content,
                Price = product.Price,
                Quantity = product.Quantity,
                Thumbnail = product.Thumbnail,
                Image = product.Image
            };

            return View("Edit", viewModel);
        }

        //EDIT : POST
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View("Edit", viewModel);
            }



            var userId = User.Identity.GetUserId();
            var product = context.Products
                .Single(g => g.Id == viewModel.Id && g.ProducerId == userId);





            product.Modify(viewModel.Name, viewModel.Content, viewModel.Price, viewModel.Quantity, viewModel.Thumbnail, viewModel.Image);
            context.SaveChanges();


            return RedirectToAction("MyProducts", "Products");
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            GlobalMethods.MaybeInitializeSession();

            var userId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }



            ViewBag.Session = SessionSingleton.Current.Cart;



            return View(product);
        }
        //DELETE :GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        //DELETE :POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("MyProducts");
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }




    }
}
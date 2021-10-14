using MagazinVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazinVirtual.Controllers
{
    public class OrderController : Controller
    {
        private MagazinVirtualEntities db = new MagazinVirtualEntities();
        // GET: Order
        Dictionary<int, int> np = new Dictionary<int, int>();
        public ActionResult Index()
        {
            
            Order o = new Order(db);
            return View(o);
        }

        public ActionResult AddOrder()
        {
            List<Produs> prod = (List<Produs>)Session["cart"];
            Dictionary<int, int> np = (Dictionary<int, int>)Session["np"];
            decimal totalcomanda = (decimal)Session["total"];
            Orders o = new Orders();
            o.CustomerAddress = "Adresa";
            o.CustomerEmail = "email@yahoo.com";
            o.CustomerName = "Popescu Ana";
            o.CustomerPhoneNumber = "0712345678";
            o.OrderDate = System.DateTime.Now;
            o.Total = (int)totalcomanda;
            db.Orders.Add(o);
            db.SaveChanges();
            foreach (Produs p in prod)
            {
                OrdersProducts op = new OrdersProducts();
                op.OrderId = db.Orders.Max(x => x.Id);
                op.ProductId = p.IdProdus;
                op.Qty = np[p.IdProdus];
                op.Price = (int)p.Price;
                op.Value = op.Qty * op.Price;
                db.OrdersProducts.Add(op);
                db.SaveChanges();
            }
            Session.Contents.RemoveAll();
            return RedirectToAction("Index", "Order");

        }
    }
}
using MagazinVirtual.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazinVirtual.Controllers
{
    public class AddToCartController : Controller
    {
        
        MagazinVirtualEntities db = new MagazinVirtualEntities();
        // GET: AddToCart  
        public ActionResult Add(int id)
        {

            if (Session["cart"] == null)
            {
                List<Produs> li = new List<Produs>();
                Dictionary<int, int> nP = new Dictionary<int, int>();
                Products prod = db.Products.Where(x => x.Id == id).First();
                Produs pprod = new Produs(prod,db);
                
                decimal sum = 0;
               
                if(nP.ContainsKey(id))
                {
                    nP[id] += 1;
                }
                else
                {
                    li.Add(pprod);
                    nP.Add(id, 1);
                }
                if (li.Count()>0)
                {
                    foreach(Produs p in li)
                    {
                        
                        sum += p.Price * nP[p.IdProdus];
                    }
                }

                Session["cart"] = li;
                //numar din fiecare produs
                Session["np"] = nP;
                Session["total"] = sum;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
                

            }
            else
            {
                List<Produs> li = (List<Produs>)Session["cart"];
                Dictionary<int, int> nP = (Dictionary<int, int>)Session["np"];
                Products prod = db.Products.Where(x => x.Id == id).First();
                Produs pprod = new Produs(prod,db);

                decimal sum = 0;
                if (nP.ContainsKey(id))
                {
                    nP[id] += 1;
                }
                else
                {
                    li.Add(pprod);
                    nP.Add(id, 1);
                }
                if (li.Count() > 0)
                {
                    foreach (Produs p in li)
                    {

                        sum += p.Price * nP[p.IdProdus];
                    }
                }
                Session["cart"] = li;
                Session["np"] = nP;
                Session["total"] = sum;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            return RedirectToAction("Myorder", "AddToCart");
        }
        public ActionResult Myorder()
        {

            return View((List<Produs>)Session["cart"]);

        }
        public ActionResult EachProductDetails(int id)
        {
            List<Produs> list = new List<Produs>();
            Products prod = db.Products.Where(x=> x.Id==id).First();
            Produs pprod = new Produs(prod,db);
            /*Products prod = new Products();
            prod.Id = m.IdProdus;
            prod.Name = m.DenumireProdus;
            //prod.ImagePath = (byte[])m.ImgSrc;
            prod.Price = m.Price;
         */
            list.Add(pprod);
            return View(list);
        }

        public ActionResult Remove(int id)
        {
            List<Produs> li = (List<Produs>)Session["cart"];
            Dictionary<int, int> nP = (Dictionary<int, int>)Session["np"];
            Products prod = db.Products.Where(x => x.Id == id).First();
            Produs pprod = new Produs(prod,db);
            if (nP[pprod.IdProdus]>1)
            {
                nP[pprod.IdProdus] -= 1;
            }
            else
            {
                li.RemoveAll(x => x.IdProdus == pprod.IdProdus);
            }
            //li.RemoveAll(x => x.Id == p.Id);
            decimal sum = (decimal)Session["total"];
            if (li.Count() > 0)
            {
                foreach (Produs pr in li)
                {

                    sum -= pprod.Price ;
                }
            }
            Session["cart"] = li;
            Session["np"] = nP;
            Session["total"] = sum;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");

        }

        public ActionResult AddSameProduct(int id)
        {
            //List<Produs> li = (List<Produs>)Session["cart"];
            Products prod = db.Products.Where(x => x.Id == id).First();
            Produs pprod = new Produs(prod, db);
            Dictionary<int, int> nP = (Dictionary<int, int>)Session["np"];
            nP[pprod.IdProdus] += 1;
            decimal sum = (decimal)Session["total"];
            //if (li.Count() > 0)
            {
               // foreach (Produs pp in li)
                {
                    sum += pprod.Price;
                }
            }

            //Session["cart"] = li;
            Session["np"] = nP;
            Session["total"] = sum;
            Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            return RedirectToAction("Myorder", "AddToCart");

        }
    }
}
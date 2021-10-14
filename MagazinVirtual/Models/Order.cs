using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MagazinVirtual;
namespace MagazinVirtual.Models
{
    public class Order
    {
        public List<OrderDetails> od = new List<OrderDetails>();
     
        public Order(MagazinVirtualEntities db)
        {
            List<Orders> ords = db.Orders.ToList();
         
            foreach (Orders x in ords)
            {
                OrderDetails ordd = new OrderDetails(db,x);
                od.Add(ordd);
            }
           
        }
    }
    public class OrderDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public decimal total { get; set; }
        public List<ProduseComanda> produse = new List<ProduseComanda>();
        public OrderDetails(MagazinVirtualEntities db, Orders ord)
        {
            Orders o = db.Orders.Where(x => x.Id == ord.Id).First();
            id = o.Id;
            name = o.CustomerName;
            address = o.CustomerAddress;
            email = o.CustomerEmail;
            phone = o.CustomerPhoneNumber;
            date = o.OrderDate.ToString("D");
            List<OrdersProducts> op = db.OrdersProducts.Where(x => x.OrderId == ord.Id).ToList();
            foreach (OrdersProducts x in op)
            {
                ProduseComanda pc = new ProduseComanda();
                pc.idprodus = x.ProductId;
                pc.denumire = x.Products.Name;
                pc.price = x.Products.Price;
                pc.qty = x.Qty;
                pc.value = x.Value;
                
                produse.Add(pc);
            }
            total = produse.Sum(x=> x.value);
        }
    }
   
    public class ProduseComanda
    {
        public int idprodus { get; set; }
        public string denumire { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public decimal value { get; set; }
        
    }

}
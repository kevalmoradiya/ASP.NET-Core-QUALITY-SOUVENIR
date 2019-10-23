using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    public class CartController : Controller
    {
        private readonly SouvenirContext _context;

        public CartController(SouvenirContext context)
        {
            _context = context;
        }
        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult Index()
        {
            
            List<string> data = new List<string>();
            List<int> dataq = new List<int>();
            List<int> dataid = new List<int>();
            float total = 0;

            string dataAsString = HttpContext.Session.GetString("QUALITYPRODUCT");
            if (!string.IsNullOrEmpty(dataAsString))
            {
                data.AddRange(dataAsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                for (int i = 0; i < data.Count; i++)
                {
                    dataq.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                    dataid.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
                    //calculate total price
                    total += dataq[i] * _context.Souvenirs.Where(so =>so.SouvenirID == dataid[i]).Select(p=>p.SouPrice).First();
                    
                }
                ViewBag.TotalCost = total;
                ViewBag.GST = (total * 15) / 100;
                ViewBag.TotalCostGST = total + ((total * 15) / 100);
                ViewBag.ItemQuantity = dataq;
                var s = _context.Souvenirs.Where(so => dataid.Contains(so.SouvenirID));
                 return View("Index",s); 


               
            }
            else
            {
                return View("~/Views/Cart/EmptyResult.cshtml");


            }


        }
        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult DeleteCartItem(int souid)
        {
            List<string> data = new List<string>();
            List<int> dataq = new List<int>();
            List<int> dataid = new List<int>();
            float total = 0;

            string dataAsString = HttpContext.Session.GetString("QUALITYPRODUCT");
           
                data.AddRange(dataAsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                for (int i = 0; i < data.Count; i++)
                {
                    dataq.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                    dataid.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
                }
                int index = dataid.IndexOf(souid);
                dataq.RemoveAt(index);
                dataid.RemoveAt(index);
                string returnf = "";
                for (int i = 0; i < dataid.Count; i++)
                {
                    returnf += dataid[i].ToString() + "/" + dataq[i].ToString() + ",";
                //total cost
                    total += dataq[i] * _context.Souvenirs.Where(so => so.SouvenirID == dataid[i]).Select(p => p.SouPrice).First();

                }
                HttpContext.Session.SetString("QUALITYPRODUCT", returnf);


                HttpContext.Session.SetInt32("CartCount", dataid.Count);
                var s = _context.Souvenirs.Where(so => dataid.Contains(so.SouvenirID));
              
                    ViewBag.TotalCost = total;
                    ViewBag.GST = (total * 15) / 100;
                    ViewBag.TotalCostGST = total + ((total * 15) / 100);
                    ViewBag.ItemQuantity = dataq;
                   

                if (dataq.Count != 0)
                { return View("Index",s); }
                else
                { return View("~/Views/Cart/EmptyResult.cshtml"); }
            

            

        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult Checkout()
        {

            
            string dataAsString = HttpContext.Session.GetString("QUALITY");
            if (!string.IsNullOrEmpty(dataAsString))
            {
                HttpContext.Session.SetString("FROMCART", "NO");
                List<string> data = new List<string>();
                List<int> dataq = new List<int>();
                List<int> dataid = new List<int>();
                float total = 0;

                string dataAs = HttpContext.Session.GetString("QUALITYPRODUCT");
                if (!string.IsNullOrEmpty(dataAs))
                {
                    data.AddRange(dataAs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    for (int i = 0; i < data.Count; i++)
                    {
                        dataq.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                        dataid.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
                        //calculate total price
                        total += dataq[i] * _context.Souvenirs.Where(so => so.SouvenirID == dataid[i]).Select(p => p.SouPrice).First();

                    }
                    ViewBag.TotalCost = total;
                    ViewBag.GST = (total * 15) / 100;
                    ViewBag.TotalCostGST = total + ((total * 15) / 100);
                    ViewBag.ItemQuantity = dataq;
                    var s = _context.Souvenirs.Where(so => dataid.Contains(so.SouvenirID));
                    return View("Checkout", s); 



                }
                else
                {
                    return View("~/Views/Cart/EmptyResult.cshtml");


                }
                
                


            }
            else
            {
                HttpContext.Session.SetString("FROMCART", "YES");
                return RedirectToAction("Login", "Login");


            }


        }
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> PayBill()
        {
            List<string> data = new List<string>();
            List<int> dataq = new List<int>();
            List<int> dataid = new List<int>();
            float total = 0;

            string dataAs = HttpContext.Session.GetString("QUALITYPRODUCT");
           
                data.AddRange(dataAs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                for (int i = 0; i < data.Count; i++)
                {
                    dataq.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                    dataid.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
                    //calculate total price
                    total += dataq[i] * _context.Souvenirs.Where(so => so.SouvenirID == dataid[i]).Select(p => p.SouPrice).First();

                }
              
                var s = _context.Souvenirs.Where(so => dataid.Contains(so.SouvenirID));
              string id= HttpContext.Session.GetString("QUALITYIDSTRING"); 
            var order = new Order();
            order.UserID =Convert.ToInt32(id);
            order.OrdDate = DateTime.Now;
            order.OrdStatus = "Waiting";
            order.OrdCost = total;
            _context.Add(order);
            await _context.SaveChangesAsync();

            for (int i=0;i<dataq.Count;i++)
            {
                var souvenir = _context.Souvenirs.Where(so => so.SouvenirID == dataid[i]).First();
                souvenir.SouQuantity = souvenir.SouQuantity - dataq[i];

                var orderitem = new OrderItem();
                orderitem.OrderID = order.OrderID;
                orderitem.SouvenirID =dataid[i];
                orderitem.OrdItemQ =dataq[i];
                _context.Add(orderitem);
                _context.Update(souvenir);

            }
            await _context.SaveChangesAsync();



            HttpContext.Session.SetString("QUALITYPRODUCT", "");
            HttpContext.Session.SetInt32("CartCount", 0);
            return View("~/Views/Cart/PayBill.cshtml");


        }
    }
}
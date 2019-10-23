using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    public class HomeController : Controller
    {
        private readonly SouvenirContext _context;

        public HomeController(SouvenirContext context)
        {
            _context = context;
        }

        
   

        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
        [ResponseCache(NoStore = true, Duration = 0)]
        public ViewResult SouvenirShopping(int item,int souid)
        {

            ViewBag.CurrentView = "SHOPPING";
            Class c = new Class()
            {
                Categoryy = GetCategoryModel(),
                Souvenirss = GetSouvenirsModel(item),
                CartProduct = GetSouCookie(souid)
            };
            if(item!=0)
            { ViewBag.SelectedCat = item; }
            return View("SouvenirShopping", c);
            
        }

        private IEnumerable<Category> GetCategoryModel()
        {
            var categoryy = _context.Categories.ToList();


            return categoryy;
        }

        private IEnumerable<Souvenir> GetSouvenirsModel(int item)
        {
            if(item!=0)
            {
                var souvenirr = _context.Souvenirs
                   .Where(m => m.CategoryID.Equals(item));
                return souvenirr;
            }
            else
            {
                var souvenirr= _context.Souvenirs.ToList();
                return souvenirr;
            }
         }
        private IEnumerable<Souvenir> GetSouvenirsModel(string searchString)
        {

            var souvenir = from s in _context.Souvenirs.Where(s=>s.SouQuantity >0)
                           select s ;

            if (!String.IsNullOrEmpty(searchString))
            {
                souvenir = souvenir.Where(s => s.SouName.Contains(searchString) || s.SouDescription.Contains(searchString));
            }
            return souvenir;
        }

        public List<int> GetSouCookie(int souid)
        {
            if(souid==0 || souid.Equals(null))
            { return null; }

            string returnf="";
            string data1 = HttpContext.Session.GetString("QUALITYPRODUCT");
            List<string> data = new List<string>();
            
            
            List<string> datat = new List<string>();
            List<int> dataq = new List<int>();
            List<int> dataid = new List<int>();
            List<int> returnlist = new List<int>();
            if (data1!=null)
            { datat.AddRange(data1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                for(int i=0;i<datat.Count;i++)
                {
                    dataq.Add(Convert.ToInt32(datat[i].Split(new char[] { '/' }).Last()));
                    dataid.Add(Convert.ToInt32(datat[i].Split(new char[] { '/' }).First()));
                }
            }
            int q = 0;
           if(dataid.Contains(souid))
            { q = dataq[dataid.IndexOf(souid)]; }
            
            if (SouQuantity(souid)-q>0)
            {
                
                if (data1 == null)
                {
                    data1=souid.ToString() +"/1";

                    HttpContext.Session.SetString("QUALITYPRODUCT", data1);
                    

                    data.AddRange(data1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                   
                    HttpContext.Session.SetInt32("CartCount", data.Count);
                }
                else if(!dataid.Contains(souid))
                {
                   
                        data1 += ","+souid.ToString() + "/1";

                        HttpContext.Session.SetString("QUALITYPRODUCT", data1);
                        string dataAsString = HttpContext.Session.GetString("QUALITYPRODUCT");

                        data.AddRange(dataAsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                       
                    HttpContext.Session.SetInt32("CartCount", data.Count);


                }
                else
                {
                    int index = dataid.IndexOf(souid);
                    dataq[index] = dataq[index] + 1;
                    
                       
                         returnf = dataid[0].ToString() + "/" + dataq[0].ToString(); 
                            for (int i = 1; i < dataid.Count; i++)
                            {
                                
                                 returnf +=","+dataid[i].ToString() + "/" + dataq[i].ToString(); 

                            }
                        
                        
                    
                    HttpContext.Session.SetString("QUALITYPRODUCT", returnf);
                    data.AddRange(returnf.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                   
                    HttpContext.Session.SetInt32("CartCount", data.Count);
                }
                    
                
            }

            List<int> dataqr = new List<int>();
            List<int> dataidr = new List<int>();
            for (int i = 0; i < data.Count; i++)
            {
                dataqr.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                dataidr.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
            }
            ViewBag.ItemQuantity = dataqr;
            return dataidr; 

            
        }

        public ActionResult IncreaseQuantity(int souid)
        {
            string returnf = "";
            string data1 = HttpContext.Session.GetString("QUALITYPRODUCT");
            List<string> data = new List<string>();


            List<string> datat = new List<string>();
            List<int> dataq = new List<int>();
            List<int> dataid = new List<int>();
            List<int> returnlist = new List<int>();
            datat.AddRange(data1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            for (int i = 0; i < datat.Count; i++)
            {
                dataq.Add(Convert.ToInt32(datat[i].Split(new char[] { '/' }).Last()));
                dataid.Add(Convert.ToInt32(datat[i].Split(new char[] { '/' }).First()));
            }
            int q = 0;
            if (dataid.Contains(souid))
            { q = dataq[dataid.IndexOf(souid)]; }

            if (SouQuantity(souid) - q <= 0)
            {
                return RedirectToAction("Index", "Cart");
            }


                int index = dataid.IndexOf(souid);

           
                dataq[index] = dataq[index] + 1;

                returnf = dataid[0].ToString() + "/" + dataq[0].ToString();
                for (int i = 1; i < dataid.Count; i++)
                {

                    returnf += "," + dataid[i].ToString() + "/" + dataq[i].ToString();

                }
                HttpContext.Session.SetString("QUALITYPRODUCT", returnf);
                data.AddRange(returnf.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                HttpContext.Session.SetInt32("CartCount", data.Count);


                List<int> dataqr = new List<int>();
                List<int> dataidr = new List<int>();
                for (int i = 0; i < data.Count; i++)
                {
                    dataqr.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                    dataidr.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
                }
                ViewBag.ItemQuantity = dataqr;
            

            return RedirectToAction("Index", "Cart");
        }

        public ActionResult DecreaseQuantity(int souid)
        {

            string returnf = "";
            string data1 = HttpContext.Session.GetString("QUALITYPRODUCT");
            List<string> data = new List<string>();


            List<string> datat = new List<string>();
            List<int> dataq = new List<int>();
            List<int> dataid = new List<int>();
            List<int> returnlist = new List<int>();
              datat.AddRange(data1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            for (int i = 0; i < datat.Count; i++)
            {
                dataq.Add(Convert.ToInt32(datat[i].Split(new char[] { '/' }).Last()));
                dataid.Add(Convert.ToInt32(datat[i].Split(new char[] { '/' }).First()));
            }
            

            
            int index = dataid.IndexOf(souid);

            if(dataq[index] > 1)
            {

                dataq[index] = dataq[index] - 1;

                returnf = dataid[0].ToString() + "/" + dataq[0].ToString();
                for (int i = 1; i < dataid.Count; i++)
                {

                    returnf += "," + dataid[i].ToString() + "/" + dataq[i].ToString();

                }
                HttpContext.Session.SetString("QUALITYPRODUCT", returnf);
                data.AddRange(returnf.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                HttpContext.Session.SetInt32("CartCount", data.Count);


                List<int> dataqr = new List<int>();
                List<int> dataidr = new List<int>();
                for (int i = 0; i < data.Count; i++)
                {
                    dataqr.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).Last()));
                    dataidr.Add(Convert.ToInt32(data[i].Split(new char[] { '/' }).First()));
                }
                ViewBag.ItemQuantity = dataqr;
            }
                
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult SouvenirSearch(string searchString)
        {
            Class c = new Class()
            {
                Categoryy = GetCategoryModel(),
                Souvenirss = GetSouvenirsModel(searchString),
                CartProduct = GetSouCookie(0)
            };


            return View("SouvenirShopping", c);
        }
        public int SouQuantity(int souid)
        {
            var souvenir = _context.Souvenirs.Where(so => so.SouvenirID == souid).First();
           
            return souvenir.SouQuantity;
        }


    }
}

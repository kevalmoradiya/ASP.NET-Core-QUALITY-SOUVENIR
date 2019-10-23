using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Models.Extended;

namespace QualitySouvenirs.Controllers
{
    public class LoginController : Controller
    {
        private readonly SouvenirContext con;

        public LoginController(SouvenirContext context)
        {
            con = context;
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //Registration POST action 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(PUser user)

        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(user.UseEmail);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion
                var newuser = new User();
                newuser.UseFName = user.UseFName;
                newuser.UseLName = user.UseLName;
                newuser.UsePno = user.UsePno;
                newuser.UseEmail = user.UseEmail;
                newuser.UsePassword = Crypto.Hash(user.UsePassword);
                newuser.UseStatus = "Enabled";
                newuser.UseAddress = user.UseAddress;
                
                    con.Users.Add(newuser);
                    con.SaveChanges();

                SendEmail(user.UseEmail);
                    message = "Registration successfully done.";
                    Status = true;
              
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }


        //Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";

                var v = con.Users.Where(a => a.UseEmail == login.UseEmail).FirstOrDefault();
                if (v != null)
                {


                    if (string.Compare(Crypto.Hash(login.UsePassword), v.UsePassword) == 0)
                    {
                        
                        
                            HttpContext.Session.SetString("QUALITY", login.UseEmail);
                    HttpContext.Session.SetString("QUALITYIDSTRING", v.UserID.ToString());
                    HttpContext.Session.SetInt32("QUALITYID", v.UserID);
                        string datastr = HttpContext.Session.GetString("FROMCART");
                        if (login.UseEmail=="admin123@gmail.com" && (string.IsNullOrEmpty(datastr) || datastr == "NO"))
                        {

                            return RedirectToAction("Admin", "Admin");
                        }
                    
                        if (string.IsNullOrEmpty(datastr) || datastr == "NO")
                        {


                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("SouvenirShopping", "Home");
                            }
                        }
                        else
                        {
                                return RedirectToAction("Checkout", "Cart");
                        
                        }




                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
           
            ViewBag.Message = message;
            
            return View();

        }

        //Logout
        [HttpGet]
        public ActionResult Logout()
        {
            //Response.Cookies.Delete("QualitySouvenir");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            
                var v = con.Users.Where(a => a.UseEmail == emailID).FirstOrDefault();
                return v != null;
            
        }

        [NonAction]
        public void SendEmail(string emailID)
        {
           
            var fromEmail = new MailAddress("qualitysouvenirs@gmail.com", "Quality Souvenir");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Quality12345"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>Welcome to Quality Souvenir.Happy shopping.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }





    }
}
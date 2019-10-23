using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;

namespace QualitySouvenirs.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly SouvenirContext _context;

        public AdminController(SouvenirContext context)
        {
            _context = context;
        }
        public IActionResult Admin()
        {
            return View();
        }

        // GET: Users
        public async Task<IActionResult> GetUser()
        {
           
            return View(await _context.Users.ToListAsync());
        }

       
       
        public async Task<IActionResult> DisabledUser(string status,int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(status=="Enabled")
            { user.UseStatus = "Disabled"; }
            else
            { user.UseStatus = "Enabled"; }
            _context.Users.Update(user);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetUser));
        }

        public async Task<IActionResult> MakeShipped(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order.OrdStatus == "Waiting")
            {
                order.OrdStatus = "Shipped";
                order.OrdSDate = DateTime.Now;
            }
           
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Orders");
        }
    }
}
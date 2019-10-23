using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Models.Extended;

namespace QualitySouvenirs.Controllers
{
    public class UsersController : Controller
    {
        private readonly SouvenirContext _context;

        public UsersController(SouvenirContext context)
        {
            _context = context;
        }

       

        // GET: Users/Details/5
        public async Task<IActionResult> Details()
        {
           

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == HttpContext.Session.GetInt32("QUALITYID"));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

      

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit()
        {
           

            var user = await _context.Users.FindAsync(HttpContext.Session.GetInt32("QUALITYID"));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UsePassword = Crypto.Hash(user.UsePassword);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            return View(user);
        }

       

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public ViewResult UserOrders()
        {
            int userid= Convert.ToInt32(HttpContext.Session.GetString("QUALITYIDSTRING"));
            UserOrder c = new UserOrder()
            {
                Userorders = GetUserOrder(userid),
                Userorderitems = GetUserOrderItem(userid)
            };
            
            return View("UserOrder", c);

        }

        private List<OrderItem> GetUserOrderItem(int userid)
        {
            List<OrderItem> orderitem = new List<OrderItem>();
            var uorder = _context.Orders.Where(m => m.UserID == userid);
            foreach(Order order in uorder)
            {
                foreach(OrderItem item in _context.OrderItems.Where(m=>m.OrderID==order.OrderID))
                {
                    orderitem.Add(item);
                }
                    

            }
            return orderitem;
            
        }

        private IEnumerable<Order> GetUserOrder(int userid)
        {
            var order = _context.Orders
                   .Where(m => m.UserID.Equals(userid));
            return order;
        }
    }
}

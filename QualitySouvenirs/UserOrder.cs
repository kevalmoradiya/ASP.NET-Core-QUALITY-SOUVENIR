using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Models;

namespace QualitySouvenirs
{
    public class UserOrder
    {
        public IEnumerable<Order> Userorders { get; set; }
        public List<OrderItem> Userorderitems { get; set; }
    }
}

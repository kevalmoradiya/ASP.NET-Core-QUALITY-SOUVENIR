using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrdDate { get; set; }
        public string OrdStatus { get; set; }
        public DateTime OrdSDate { get; set; }
        public double OrdCost { get; set; }

        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}

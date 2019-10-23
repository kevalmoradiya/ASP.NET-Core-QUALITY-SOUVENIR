using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int SouvenirID { get; set; }
        public int OrdItemQ { get; set; }

        public Order Order { get; set; }
        public Souvenir Souvenir { get; set; }

    }
}

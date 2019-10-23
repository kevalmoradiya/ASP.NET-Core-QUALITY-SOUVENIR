using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupName { get; set; }
        public string SupPno { get; set; }
        public string SupEmail { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }

    }
}

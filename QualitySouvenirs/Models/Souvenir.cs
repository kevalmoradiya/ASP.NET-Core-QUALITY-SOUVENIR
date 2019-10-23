using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Souvenir
    {
        public int SouvenirID { get; set; }
        public string SouName { get; set; }
        public float SouPrice { get; set; }
        public string SouDescription { get; set; }
        public byte[] SouImage { get; set; }
        public int SouQuantity { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }

    }
}

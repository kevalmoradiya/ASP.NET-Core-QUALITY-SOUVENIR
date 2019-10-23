using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public partial class User
    {
        public int UserID { get; set; }
        public string UseFName { get; set; }
        public string UseLName { get; set; }
        public string UsePno { get; set; }
        public string UseEmail { get; set; }
        public string UsePassword { get; set; }
        public string UseStatus { get; set; }
        public string UseAddress { get; set; }
        
        public ICollection<Order> Orders { get; set; }
    }
}

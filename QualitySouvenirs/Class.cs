using QualitySouvenirs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs
{
    public class Class
    {
        public IEnumerable<Souvenir> Souvenirss { get; set; }
        public IEnumerable<Category> Categoryy { get; set; }
        public List<int> CartProduct { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "Category Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        [StringLength(20, ErrorMessage = "Maximum 20 character")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Use letters only please")]
        public string CatName { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}

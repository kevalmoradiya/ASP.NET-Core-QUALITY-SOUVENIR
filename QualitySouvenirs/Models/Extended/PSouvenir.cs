using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models.Extended
{
    public class PSouvenir
    {
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name required")]
        [DataType(DataType.Text)]
        public string SouName { get; set; }
        [Display(Name = "Price")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Price required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        public float SouPrice { get; set; }
        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description required")]
        [DataType(DataType.Text)]
        public string SouDescription { get; set; }
        [Display(Name = "Image (150px*150px)")]
        [Required]
        public IFormFile image { get; set; }
        [Display(Name = "Quantity")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid quantity")]
        public int SouQuantity { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models.Extended
{
    public class EditSouvenir
    {
        
        public int SouvenirID { get; set; }
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
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image required")]
        [DataType(DataType.ImageUrl)]
        public IFormFile image { get; set; }

        public byte[] sImage { get; set; }

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

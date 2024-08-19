using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name = "Urun Id")]
        [BindNever]
        public int ProductId { get; set; }

        [Display(Name = "Urun Adı")]
        [Required(ErrorMessage = "Ürün Adı Giriniz.")]

        public string? Name { get; set; } = string.Empty;

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Ürün Fiyatı Giriniz.")]
        public decimal? Price { get; set; }

        [Display(Name = "Urun Gorseli")]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "Ürün Listelensin")]
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Seçiniz")]        
        public int? CategoryId { get; set; }
    }
}
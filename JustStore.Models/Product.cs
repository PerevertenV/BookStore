using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustStore.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Author")]
        public string Author { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("List Price")]
        [Range(0, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [DisplayName("Price 1 - 50")]
        [Range(0, 1000)]
        public double Price { get; set; }

        [Required]
        [DisplayName("Price for 50+")]
        [Range(0, 1000)]
        public double Price50 { get; set; }

        [Required]
        [DisplayName("Price for 100+")]
        [Range(0, 1000)]
        public double Price100 { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
    }
}

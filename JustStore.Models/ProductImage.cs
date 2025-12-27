using System.ComponentModel.DataAnnotations;

namespace JustStore.Models
{
    public class ProductImage
    {
        public int ID { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

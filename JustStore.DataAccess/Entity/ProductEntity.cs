using JustStore.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [MaxLength(30)]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double ListPrice { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Price50 { get; set; }

        [Required]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Category { get; set; }

        public List<ProductImagesEntity> ProductImages { get; set; }
    }
}

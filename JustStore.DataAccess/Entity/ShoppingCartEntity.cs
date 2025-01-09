using JustStore.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("ShoppingCart")]
    public class ShoppingCartEntity
    {
        [Key]
        public int Id { get; set; }

        public int Count { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}

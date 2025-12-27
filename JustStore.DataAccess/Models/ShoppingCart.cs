using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entity;

namespace DataAccess.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; } = default!;

        public string ApplicationUserId { get; set; } = default!;

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } = default!;

        public double Price { get; set; }
    }
}

﻿using JustStore.Models;
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
    [Table("OrderDetails")]
    public class OrderDetailsEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey(nameof(OrderHeaderId))]
        [ValidateNever]
        public OrderHeaderEntity OrderHeader { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}

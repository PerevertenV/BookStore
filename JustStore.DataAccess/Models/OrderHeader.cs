using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = default!;

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } = default!;

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = default!;
        [Required]
        public string StreetAddress { get; set; } = default!;
        [Required]
        public string City { get; set; } = default!;
        [Required]
        public string State { get; set; } = default!;
        [Required]
        public string PostalCode { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;

    }
}

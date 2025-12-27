using System.ComponentModel.DataAnnotations;

namespace JustStore.Models
{
    public class Company
    {

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        [MaxLength(13)]
        public string? PhoneNumber { get; set; }
    }
}

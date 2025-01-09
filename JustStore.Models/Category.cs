using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustStore.Models
{
    public class Category
    {
       
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 - 100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}

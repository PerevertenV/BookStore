using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("Category")]
    public class CategoryEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
       
        public int DisplayOrder { get; set; }
    }
}

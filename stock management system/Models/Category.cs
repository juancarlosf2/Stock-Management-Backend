using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, DataType(DataType.DateTime), DisplayName("CreatedDate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required, DataType(DataType.DateTime), DisplayName("Updated")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        [DisplayName("Description")]
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

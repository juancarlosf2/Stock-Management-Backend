using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class Stock
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey("ProductSku"), DisplayName("Product")]
        public string ProductSku { get; set; }

        [Required, DisplayName("Quantity")]
        public int Quantity { get; set; }

        [Required, DataType(DataType.DateTime), DisplayName("Updated")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; }
    }
}

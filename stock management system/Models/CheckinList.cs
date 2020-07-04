using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class CheckinList
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductSku"), DisplayName("Product")]
        public string ProductSku { get; set; }

        [ForeignKey("CheckinId"), DisplayName("Checkin")]
        public int CheckinId { get; set; }

        [Required, DisplayName("Quantity")]
        public int Quantity { get; set; }


        [Required, DisplayName("Unit Price")]
        public int UnitPrice { get; set; }

        [Required, DisplayName("Total Price")]
        public int TotalPrice { get; set; }

        public Product Product { get; set; }

        public Checkin Checkin { get; set; }
    }
}

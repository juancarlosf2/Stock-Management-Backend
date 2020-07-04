using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class CheckoutList
    {

        [Key, ForeignKey("ProductSku"), DisplayName("Product")]
        public string ProductSku { get; set; }

        [Key, ForeignKey("CheckoutId"), DisplayName("Checkout")]
        public int CheckoutId { get; set; }

        [Required, DisplayName("Quantity")]
        public int Quantity { get; set; }

        public Product Product { get; set; }

        public Checkout Checkout { get; set; }
    }
}

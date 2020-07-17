using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace stock_management_system.Models
{
    public class Product
    {

        [Key, Required]
        public string Sku { get; set; }


        [ForeignKey("CategoryId"), DisplayName("Category")]
        public int? CategoryId { get; set; }

        [Required, MaxLength(60), DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }
            
        [DisplayName("Photo Uri")]
        public string PhotoUri { get; set; }

        [DisplayName("Alert Quantity")]
        public int? AlertQuantity { get; set; }

        [DisplayName("Selling Price")]
        public int? SellingPrice { get; set; }
        
        [DisplayName("Margin Profitability")]
        public int? MarginProfitability { get; set; }

        [DisplayName("Units")]
        public int Units { get; set; }

        public Category Category { get; set; }

        public ICollection<CheckinList> CheckInLists { get; set; }

        public ICollection<CheckoutList> CheckoutLists { get; set; }

        public Stock Stock { get; set; }


    }
}

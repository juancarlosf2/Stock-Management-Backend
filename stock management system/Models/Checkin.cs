using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_management_system.Models
{
    public class Checkin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EmployeeId"), DisplayName("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("SupplierId"), DisplayName("Supplier")]
        public int SupplierId { get; set; }

        [Required, DisplayName("Sub Total")]
        public int SubTotal { get; set; }

        [DisplayName("Discount")]
        public int? Discount { get; set; }

        [Required, DisplayName("Grand Total")]
        public int GrandTotal { get; set; }

        [Required, DataType(DataType.DateTime), DisplayName("Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Employee Employee { get; set; }

        public Supplier Supplier { get; set; }

        public ICollection<CheckinList> CheckinLists { get; set; }
    }
}

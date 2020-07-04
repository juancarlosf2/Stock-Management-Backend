using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class Checkout
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EmployeeId"), DisplayName("Employee")]
        public int EmployeeId { get; set; }

        [Required, DataType(DataType.DateTime), DisplayName("Order Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Employee Employee { get; set; }

        public ICollection<CheckoutList> CheckoutLists { get; set; }
    }
}

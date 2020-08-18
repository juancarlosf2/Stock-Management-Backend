using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class Employee
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(30), DisplayName("Name")]
        public string Name { get; set; }

        [Required, MaxLength(30), DisplayName("Lastname")]
        public string Lastname { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(100), DisplayName("Email")]
        public string Email { get; set; }

        [Display(Name = "Phone"), Range(0, 9999999999), DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

        [Required, DataType(DataType.DateTime), DisplayName("CreatedDate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required, DataType(DataType.DateTime), DisplayName("Updated")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public ICollection<Checkin> CheckIn { get; set; }

        public ICollection<Checkout> Checkout { get; set; }


    }
}

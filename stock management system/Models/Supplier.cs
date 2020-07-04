using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock_management_system.Models
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(60), DisplayName("Name")]
        public string Name { get; set; }

        [Display(Name = "Phone Number"), MinLength(10), MaxLength(10), Range(0, 9999999999), DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(100), DisplayName("Email")]
        public string Email { get; set; }

        public ICollection<Checkin> Checkin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Booking = new HashSet<Booking>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Fornavn")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Efternavn")]
        public string Lastname { get; set; }

        [StringLength(100)]
        [Display(Name = "E-mailadresse")]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Telefonnummer")]
        public string Phone { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}

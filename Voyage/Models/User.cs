using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("User")]
    public partial class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Fornavn")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Efternavn")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "E-mailadresse")]
        public string Email { get; set; }

        [StringLength(12)]
        [Display(Name = "Telefonnummer")]
        public string Phone { get; set; }

        [Required]
        [StringLength(45)]
        public string Password { get; set; }
    }
}

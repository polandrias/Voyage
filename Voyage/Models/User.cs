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
        public string Firstname { get; set; }

        [Required]
        [StringLength(100)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [Required]
        [StringLength(45)]
        public string Password { get; set; }
    }
}

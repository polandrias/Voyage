using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    public partial class Status
    {
        public Status()
        {
            Booking = new HashSet<Booking>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Status")]
        public string Name { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}

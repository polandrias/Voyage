using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("Theatre")]
    public partial class Theatre
    {
        public Theatre()
        {
            Show = new HashSet<Show>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Sal")]
        public string Name { get; set; }

        [Display(Name = "Pladser")]
        public int Seats { get; set; }

        public virtual ICollection<Show> Show { get; set; }
    }
}

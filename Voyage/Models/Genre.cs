using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("Genre")]
    public partial class Genre
    {
        public Genre()
        {
            Movie = new HashSet<Movie>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }
}

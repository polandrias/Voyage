using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("Movie")]
    public partial class Movie
    {
        public Movie()
        {
            Show = new HashSet<Show>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string PosterPath { get; set; }

        [Required]
        [StringLength(100)]
        public string BigPosterPath { get; set; }

        public int Duration { get; set; }

        [StringLength(255)]
        public string Embed { get; set; }

        public decimal? Rating { get; set; }

        [Required]
        [StringLength(100)]
        public string Actor { get; set; }

        [Column("3D")]
        public bool C3D { get; set; }

        [Required]
        [StringLength(100)]
        public string Language { get; set; }

        public DateTime? Premiere { get; set; }

        public int? Release { get; set; }

        public int GenreId { get; set; }

        public bool Highlighted { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Show> Show { get; set; }
    
    }
}

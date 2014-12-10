using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web;

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
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [StringLength(100)]
        [Display(Name = "Plakat (portrætformat)")]
        public string PosterPath { get; set; }

        [StringLength(100)]
        [Display(Name = "Plakat (landskabsformat)")]
        public string BigPosterPath { get; set; }

        [Display(Name = "Varighed (min.)")]
        public int Duration { get; set; }

        [StringLength(255)]
        [Display(Name = "Embedkode")]
        public string Embed { get; set; }

        [Display(Name = "Bedømmelse")]
        public decimal? Rating { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Skuespiller")]
        public string Actor { get; set; }

        [Column("3D")]
        [Display(Name = "3D-format")]
        public bool C3D { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Sprog")]
        public string Language { get; set; }

        [Display(Name = "Premieredato")]
        public DateTime? Premiere { get; set; }

        [Display(Name = "Udgivelsesår")]
        public int? Release { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public bool Highlighted { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Show> Show { get; set; }
    
    }
}
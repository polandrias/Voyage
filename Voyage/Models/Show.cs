using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("Show")]
    public partial class Show
    {
        public Show()
        {
            Booking = new HashSet<Booking>();
        }

        public int ID { get; set; }

        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Visningstidspunkt.")]
        public DateTime Time { get; set; }

        public bool? VIP { get; set; }

        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Display(Name = "Titel")]
        public int MovieId { get; set; }

        [Display(Name = "Sal ???")]
        public int TheatreId { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }

        public virtual Movie Movie { get; set; }

        [Display(Name = "Sal")]
        public virtual Theatre Theatre { get; set; }
    }
}

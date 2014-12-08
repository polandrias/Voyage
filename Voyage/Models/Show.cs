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

        [DisplayFormat(DataFormatString = "{0:dddd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        public bool? VIP { get; set; }

        public decimal Price { get; set; }

        public int MovieId { get; set; }

        public int TheatreId { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Theatre Theatre { get; set; }
    }
}

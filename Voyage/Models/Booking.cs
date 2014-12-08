using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Voyage.Models
{
    [Table("Booking")]
    public partial class Booking
    {
        public int ID { get; set; }

        public decimal Price { get; set; }

        public int Seats { get; set; }

        public int ShowId { get; set; }

        public int StatusId { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Show Show { get; set; }

        public virtual Status Status { get; set; }
    }
}

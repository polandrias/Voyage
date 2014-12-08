namespace Voyage.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Voyage.Models;

    public partial class VoyageContext : DbContext
    {
        public VoyageContext()
            : base("name=VoyageContext")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Theatre> Theatres { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(e => e.Price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Movie)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.PosterPath)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.BigPosterPath)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.Embed)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.Rating)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Movie>()
                .Property(e => e.Actor)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Show)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Show>()
                .Property(e => e.Price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Show>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Show)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theatre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Theatre>()
                .HasMany(e => e.Show)
                .WithRequired(e => e.Theatre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}

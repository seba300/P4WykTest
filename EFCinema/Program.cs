using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EFCinema
{
    [Table("Cinema")]
    public class CinemaModel
    {
        [Key]
        public int CinemaId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoomModel> Rooms { get; set; }
        public virtual AddressModel Address { get; set; }
    }

    [Table("Room")]
    public class RoomModel
    { 
        [Key]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int SeatNumbers { get; set; }

        public virtual CinemaModel Cinema { get; set; }
        public virtual ICollection<PlaceModel> Place { get; set; }
    }

    [Table("Place")]
    public class PlaceModel
    { 
        [Key]
        public int PlaceId { get; set; }
        public string Row { get; set; }
        public int Number { get; set; }

        public virtual RoomModel Room { get; set; }
        public virtual ICollection<TicketModel> Ticket { get; set; }

    }

    [Table("Seance")]
    public class SeanceModel
    {
        [Key]
        public int SeanceId { get; set; }
        public string MovieName { get; set; }
        public DateTime MovieStart { get; set; }

       public virtual ICollection<TicketModel> Ticket { get; set; }
       
    }

    [Table("Ticket")]
    public class TicketModel
    {
        [Key]
        public int TicketId { get; set; }
        public decimal Price { get; set; }

        public virtual PlaceModel Place { get; set; }
        public virtual SeanceModel Seance { get; set; }
    }

    [Table("Address")]
    public class AddressModel
    {
        [Key]
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual CinemaModel Cinema { get; set; }
    }

    public class CinemaContext : DbContext
    {
        public CinemaContext() : base("CinemaContext") { }
        public virtual DbSet<CinemaModel> Cinemas { get; set; }
        public virtual DbSet<RoomModel> Rooms { get; set; }
        public virtual DbSet<PlaceModel> Places { get; set; }
        public virtual DbSet<SeanceModel> Seances { get; set; }
        public virtual DbSet<TicketModel> Tickets { get; set; }
        public virtual DbSet<AddressModel> Addresses { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            CinemaContext c1 = new CinemaContext();
            c1.Cinemas.Add(new CinemaModel { CinemaId = 1, Name = "Multikino" });
            c1.Addresses.Add(new AddressModel { CinemaId = 1, City = "Warszawa", Street = "BakerStreet", HomeNumber = "12", PhoneNumber = 123131, Email = "adas@asda.pl" });


        c1.SaveChanges();
        }
    }
}

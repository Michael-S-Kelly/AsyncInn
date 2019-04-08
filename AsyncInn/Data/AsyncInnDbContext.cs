using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }
        public DbSet<Hotel> Hotel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomAmenities>().HasKey(ra => new { ra.RoomID, ra.AmenitiesID });
            modelBuilder.Entity<HotelRoom>().HasKey(hr => new { hr.HotelID, hr.RoomNumber });

            modelBuilder.Entity<Amenities>().HasData(
                new Amenities
                {
                    ID = 1,
                    Name = "King Bed"
                },
                new Amenities
                {
                    ID = 2,
                    Name = "Queen Bed"
                },
                new Amenities
                {
                    ID = 3,
                    Name = "60 inch TV"
                },
                new Amenities
                {
                    ID = 4,
                    Name = "Mini-Frige"
                },
                new Amenities
                {
                    ID = 5,
                    Name = "Hot Tub"
                }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    ID = 1,
                    Name = "Honeymoon Suite",
                    Layout = Layout.OneBedroom
                },
                new Room
                {
                    ID = 2,
                    Name = "Kyoto Chambers",
                    Layout = Layout.Studio
                },
                new Room
                {
                    ID = 3,
                    Name = "Harley's Spot",
                    Layout = Layout.Studio
                },
                new Room
                {
                    ID = 4,
                    Name = "Pluto's Dive",
                    Layout = Layout.TwoBedroom
                },
                new Room
                {
                    ID = 5,
                    Name = "Port Proxima Centari",
                    Layout = Layout.OneBedroom
                },
                new Room
                {
                    ID = 6,
                    Name = "Bull Room",
                    Layout = Layout.OneBedroom
                }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    ID = 1,
                    Name = "Paris Under The Moon",
                    StreetAddress = "123 Paris Pl N",
                    City = "Miami",
                    State = "Florida",
                    Phone = "(234) 345-6789"
                },
                new Hotel
                {
                    ID = 2,
                    Name = "Hotel of the Raising Sun",
                    StreetAddress = "432 Main Blvd.",
                    City = "Las Vegas",
                    State = "Nevada",
                    Phone = "(345) 456-5678"
                },
                new Hotel
                {
                    ID = 3,
                    Name = "HyWay to Hell",
                    StreetAddress = "962 Hellsgate Way N.",
                    City = "Hell",
                    State = "Montana",
                    Phone = "(456) 567-6789"
                },
                new Hotel
                {
                    ID = 4,
                    Name = "Orion's Retreat",
                    StreetAddress = "876 1st AVE SW",
                    City = "Rosswell",
                    State = "New Mexico",
                    Phone = "(456) 567-6789"
                },
                new Hotel
                {
                    ID = 5,
                    Name = "Wallstreet Retreat",
                    StreetAddress = "455 Wall St",
                    City = "New York",
                    State = "New York",
                    Phone = "(567) 678-7890"
                }
                );

        }

        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Airly.Models;

namespace Airly.Data
{
    public class AirlyContext : DbContext
    {
        public AirlyContext(DbContextOptions<AirlyContext> options)
            : base(options) { }
        
        public DbSet<User>       Users        { get; set; }
        public DbSet<Traveler>   Travelers    { get; set; }
        public DbSet<Ticket>     Tickets      { get; set; }
        public DbSet<Connection> Connections  { get; set; }
        public DbSet<Airport>    Airports     { get; set; }
        public DbSet<Location>   Locations    { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Ticket>()
                .HasKey(t => new { t.TravelerId, t.ConnectionId });
            
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.FromAirport)
                .WithMany(a => a.DepartingConnections)
                .HasForeignKey(c => c.FromAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.ToAirport)
                .WithMany(a => a.ArrivingConnections)
                .HasForeignKey(c => c.ToAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@airly.com", PasswordHash = "21232f297a57a5a743894a0e4a801fc3"} //password: admin; cc03e747a6afbbcbf8be7668acfebee5 <- test123
            );
            
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, City = "Kraków", Country = "Poland", ImgUrl = "https://media.krakow.travel/photos/18784/xxl.jpg", Description = "Kraków, officially the Royal Capital City of Kraków, is the second-largest and one of the oldest cities in Poland. Situated on the Vistula River in Lesser Poland Voivodeship, the city has a population of 804,237 (2023), with approximately 8 million additional people living within a 100 km (62 mi) radius." },
                new Location { Id = 2, City = "London", Country = "England", ImgUrl = "https://res.cloudinary.com/aenetworks/image/upload/c_fill,ar_2,w_3840,h_1920,g_auto/dpr_auto/f_auto/q_auto:eco/v1/topic-london-gettyimages-760251843-feature?_a=BAVAZGDX0", Description = "London is the capital and largest city of both England and the United Kingdom, with a population of 8,866,180 in 2022. Its wider metropolitan area is the largest in Western Europe, with a population of 14.9 million." }
            );
        }
    }
}

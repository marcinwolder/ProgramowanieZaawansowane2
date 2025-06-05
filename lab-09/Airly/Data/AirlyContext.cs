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
            : base(options)
        {
        }

        public DbSet<Airly.Models.User> User { get; set; } = default!;
        public DbSet<Airly.Models.Destination> Destination { get; set; } = default!;
        public DbSet<Airly.Models.Ticket> Ticket { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Email = "test@test.test", Password = "cc03e747a6afbbcbf8be7668acfebee5", Id = 1 }
            );

            modelBuilder.Entity<Destination>().HasData(
                new Destination { Id = 1, City = "Kraków", Price = 150, ImgUrl = "https://media.krakow.travel/photos/18784/xxl.jpg", Description = "Kraków, officially the Royal Capital City of Kraków, is the second-largest and one of the oldest cities in Poland. Situated on the Vistula River in Lesser Poland Voivodeship, the city has a population of 804,237 (2023), with approximately 8 million additional people living within a 100 km (62 mi) radius." },
                new Destination { Id = 2, City = "London", Price = 200, ImgUrl = "https://res.cloudinary.com/aenetworks/image/upload/c_fill,ar_2,w_3840,h_1920,g_auto/dpr_auto/f_auto/q_auto:eco/v1/topic-london-gettyimages-760251843-feature?_a=BAVAZGDX0", Description = "London is the capital and largest city of both England and the United Kingdom, with a population of 8,866,180 in 2022. Its wider metropolitan area is the largest in Western Europe, with a population of 14.9 million." }
            );
        }
    }
}

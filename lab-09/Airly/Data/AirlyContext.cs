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
        public AirlyContext (DbContextOptions<AirlyContext> options)
            : base(options)
        {
        }

        public DbSet<Airly.Models.User> User { get; set; } = default!;
        public DbSet<Airly.Models.Destination> Destination { get; set; } = default!;
    }
}

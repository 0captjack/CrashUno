using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Models
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base (options)
        {
            //
        }

        public DbSet<Crash> Crash { get; set; }

        public DbSet<Location> Location { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Crash>().HasData(
                new Crash
                {
                    crash_id = 1,
                    crash_datetime = "2019 - 11 - 27T13:33:00.000",
                    route = 352012,
                    milepoint = 1,
                });

        }

    }
}

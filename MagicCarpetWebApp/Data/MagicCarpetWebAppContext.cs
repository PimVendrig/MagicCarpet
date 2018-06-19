using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MagicCarpetWebApp.Models;

namespace MagicCarpetWebApp.Models
{
    public class MagicCarpetWebAppContext : DbContext
    {
        public MagicCarpetWebAppContext (DbContextOptions<MagicCarpetWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<MagicCarpetWebApp.Models.ConcertInfo> ConcertInfoes { get; set; }
        public DbSet<ConcertLocation> ConcertLocations { get; set; }
        public DbSet<MagicCarpetWebApp.Models.Reservation> Reservations { get; set; }
    }
}

using System.Data.Entity;

namespace LicnaKarta.Data
{
    public class LicnaKartaDbContext : DbContext
    {
        public LicnaKartaDbContext()
            : base("NasaKonekcija")
        {
        }

        public DbSet<Gradjanin> Gradjani { get; set; }
        public DbSet<Zahtev> Zahtevi { get; set; }
        public DbSet<RoditeljStaratelj> RoditeljiStaratelji { get; set; }
    }
}
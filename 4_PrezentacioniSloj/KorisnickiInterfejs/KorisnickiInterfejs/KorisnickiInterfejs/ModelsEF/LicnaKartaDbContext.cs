using System.Data.Entity;

namespace KorisnickiInterfejs.ModelsEF
{
    public class LicnaKartaDbContext : DbContext
    {
        public LicnaKartaDbContext()
            : base("NasaKonekcija")
        {
        }

        public DbSet<GradjaninEF> Gradjani { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace KTRS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Koltuk> Koltuklar { get; set; }
        public DbSet<Kat> Katlar { get; set; }

        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<Yetkili> Yetkililer { get; set; }
    }

    

    

    

    

}

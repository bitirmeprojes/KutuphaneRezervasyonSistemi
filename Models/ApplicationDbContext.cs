using Microsoft.EntityFrameworkCore;

namespace KTRS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Koltuk> Koltuklar { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<Yetkili> Yetkililer { get; set; }
    }

    public class Ogrenci
    {
        public string ogrenciID { get; set; }
        public string rezervasyonNo { get; set; }
        public int cikisSaat { get; set; }
        public bool girisDurumu { get; set; }
        public string koltukNo { get; set; }
        public bool QRokutmaDurumu { get; set; }
        public int randevuBaslangicSaat { get; set; }
        public int randevuBitisSaat { get; set; }
    }

    public class Koltuk
    {
        public int Id { get; set; }
        public bool Durum { get; set; }
        public string koltukNo { get; set; } // charvar(5) yerine string
    }

    public class Rezervasyon
    {
        public string RezervasyonID { get; set; } // charvar(15) yerine string
        public bool Durum { get; set; }
        public string ogrenciID { get; set; } // charvar(5) yerine string
    }

    public class Yetkili
    {
        public string YetkiliID { get; set; } // charvar(15) yerine string
    }

}

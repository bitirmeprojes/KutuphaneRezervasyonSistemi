using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Rezervasyon
    {
        [Key]
        public string RezervasyonID { get; set; }  // Guid veya benzeri

        public string ogrenciID { get; set; }      // Rezervasyonu yapan öğrenci
        public string KoltukNo { get; set; }       // Hangi koltuk rezerve edildi?

        public bool Durum { get; set; } = true;    // Rezervasyon geçerli mi vb.
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}

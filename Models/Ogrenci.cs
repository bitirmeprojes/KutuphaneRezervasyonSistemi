using System.ComponentModel.DataAnnotations;


namespace KTRS.Models
{
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
}

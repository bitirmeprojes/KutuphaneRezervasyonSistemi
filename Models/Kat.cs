using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Kat
    {
        [Key]
        public int Id {  get; set; }
        public int KatNo { get; set; }
        public string Ad { get; set; }
        public int MaxRow { get; set; }
        public int MaxCol { get; set; }

        // Bu kat için veritabanından çektiğimiz tüm koltuklar
        public List<Koltuk> Koltuklar { get; set; } = new List<Koltuk>();
    }
}

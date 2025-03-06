using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Koltuk
    {
        public int Id { get; set; }

        // Hangi katta olduğunu belirtmek için
        public int KatNo { get; set; }
        public string koltukNo { get; set; }
        // Satır ve sütun bilgisi
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        // Koltuk boş mu dolu mu, rezervasyon durumu
        public bool Durum { get; set; } = true;

        // Opsiyonel açıklama
        [StringLength(100)]
        public string? Aciklama { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Koltuk
    {
        [Key]
        public int Id { get; set; }

        // Hangi kat
        public int KatId { get; set; }
        [ValidateNever]
        public Kat Kat { get; set; }

        // Koltuk koordinatı (örneğin 0..7 satır, 0..3 sütun)
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        // Koordinatlar (piksel, orantı, vs.)
        [ValidateNever]
        public int XCoord { get; set; }
        [ValidateNever]
        public int YCoord { get; set; }

        // Koltukun durumu
        // true: Dolu, false: Boş gibi de tanımlayabilirsiniz. İhtiyaca göre
        public bool Durum { get; set; } = false;

        // Koltuk no veya özel adı
        [StringLength(50)]
        public string KoltukNo { get; set; }

        // İsteğe bağlı açıklama
        public string Aciklama { get; set; }
    }
}

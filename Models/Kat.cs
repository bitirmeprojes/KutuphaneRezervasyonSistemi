using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Kat
    {
        [Key]
        public int Id { get; set; }

        // Hangi blokta
        public int BlockId { get; set; }
        [ValidateNever]
        public Block Block { get; set; }

        // Kat numarası veya kat adı
        public int KatNo { get; set; }

        // Navigation property (1 katın birden çok koltuğu)
        [ValidateNever]
        public ICollection<Koltuk> Koltuklar { get; set; }
    }
}

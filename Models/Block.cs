using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; } // A Blok, B Blok vb.

        // İsteğe bağlı başka alanlar da ekleyebilirsiniz
        // Örneğin bina kodu, açıklama vs.

        // Navigation property (1 bloğun birden çok Kat’ı olabilir)
        [ValidateNever]
        public ICollection<Kat> Kats { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilletterieAPI.Models
{
    public class Achat
    {
        [Key]
        public int AchatId { get; set; }
        public DateTime DateAchat { get; set; } = DateTime.Now;

        [Required]
        public int QuantiteAchetee { get; set; }

       
       
        public int TypeBilletId { get; set; }
        public TypeBillet TypeBillet { get; set; }

       
        public int SpectateurId { get; set; }
        public Spectateur Spectateur { get; set; }
    }
}
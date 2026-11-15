using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class Evenement
    {           
        [Key]
        public int EvenementId { get; set; }
        [Required]
        [StringLength(100)]

        public string Titre { get; set; }
        public DateTime DateEvenement { get; set; }

        public int LieuId { get; set; }
        public Lieu Lieu { get; set; }

        public ICollection<TypeBillet> TypeBillets { get; set; }
    }
}
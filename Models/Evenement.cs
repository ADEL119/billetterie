using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class Evenement
    {
        public int EvenementId { get; set; }
        [Required]
        public string Titre { get; set; }
        public DateTime DateEvenement { get; set; }

        // Clé étrangère vers Lieu
        public int LieuId { get; set; }
        public Lieu? Lieu { get; set; }

        public ICollection<TypeBillet>? TypeBillets { get; set; }
    }
}
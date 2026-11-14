using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class Lieu
    {
        public int LieuId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Adresse { get; set; }
        public int Capacite { get; set; }

        public ICollection<Evenement>? Evenements { get; set; }
    }
}
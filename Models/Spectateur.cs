using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class Spectateur
    {
        public int SpectateurId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }

        public ICollection<Achat>? Achats { get; set; }
    }
}
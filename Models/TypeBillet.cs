using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class TypeBillet
    {
        public int TypeBilletId { get; set; }
        [Required]
        public string Libelle { get; set; } // ex: VIP, Standard
        public decimal Tarif { get; set; }
        public int QuantiteStock { get; set; }

        // Clé étrangère vers Evenement
        public int EvenementId { get; set; }
        public Evenement? Evenement { get; set; }

        public ICollection<Achat>? Achats { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class TypeBillet
    {
        public int TypeBilletId { get; set; }
        [Required]
                [StringLength(10)]

        public string Libelle { get; set; } 
        public decimal Tarif { get; set; }
        public int QuantiteStock { get; set; }

        public int EvenementId { get; set; }
        public Evenement Evenement { get; set; }

        public ICollection<Achat> Achats { get; set; }= new List<Achat>(); //éviter null exception
    }
}
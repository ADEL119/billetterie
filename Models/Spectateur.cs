using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class Spectateur
    {
        public int SpectateurId { get; set; }
        [Required]

         [StringLength(20)]

        public string Nom { get; set; }
        [Required]
                [StringLength(20)]

        public string Prenom { get; set; }
        [Required, EmailAddress]
                [StringLength(100)]

        public string Email { get; set; }

       

        public ICollection<Achat> Achats { get; set; }  = new List<Achat>(); //éviter null exception
    }
}
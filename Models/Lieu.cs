using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.Models
{
    public class Lieu
    {   

        [Key]
        public int LieuId { get; set; }
        [Required]
        [StringLength(20)]

        public string Nom { get; set; }
              
         [Required]
        public string Adresse { get; set; }


        public ICollection<Evenement> Evenements { get; set; }
    }
}
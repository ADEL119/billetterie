using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BilletterieAPI.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username obligatoire")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
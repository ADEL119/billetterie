namespace BilletterieAPI.DTOs
{
    public class EvenementDTO
    {
        public int EvenementId { get; set; }
        public string Titre { get; set; }
        public DateTime DateEvenement { get; set; }
        
        // Propriété issue de la relation avec Lieu (aplatie)
        public string LieuNom { get; set; }
    }
}
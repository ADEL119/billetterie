namespace BilletterieAPI.DTOs
{
    public class TypeBilletDTO
    {
        public int TypeBilletId { get; set; }
        public string Libelle { get; set; }
        public decimal Tarif { get; set; }
        public int QuantiteStock { get; set; }
        
        // Propriété issue de la relation avec Evenement (aplatie)
        public string EvenementTitre { get; set; }
    }
}
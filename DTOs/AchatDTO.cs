namespace BilletterieAPI.DTOs
{
    public class AchatDTO
    {
        public int AchatId { get; set; }
        public DateTime DateAchat { get; set; }
        public int QuantiteAchetee { get; set; }
        
        // Propriétés issues des relations (aplaties)
        public string TypeBilletLibelle { get; set; }
        public string SpectateurNom { get; set; }
        public string SpectateurPrenom { get; set; }
    }
}
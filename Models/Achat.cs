namespace BilletterieAPI.Models
{
    public class Achat
    {
        public int AchatId { get; set; }
        public DateTime DateAchat { get; set; } = DateTime.Now;
        public int QuantiteAchetee { get; set; }

        // Clé étrangère vers TypeBillet
        public int TypeBilletId { get; set; }
        public TypeBillet? TypeBillet { get; set; }

        // Clé étrangère vers Spectateur
        public int SpectateurId { get; set; }
        public Spectateur? Spectateur { get; set; }
    }
}
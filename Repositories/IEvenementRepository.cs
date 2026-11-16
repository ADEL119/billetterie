using BilletterieAPI.Models;

namespace BilletterieAPI.Repositories
{
    public interface IEvenementRepository
    {
        Task<IEnumerable<Evenement>> GetEvenements();
        Task<Evenement> GetEvenement(int evenementId);
        Task<Evenement> AddEvenement(Evenement evenement);
        Task<Evenement> UpdateEvenement(Evenement evenement);
        Task<Evenement> DeleteEvenement(int evenementId);
        Task<IEnumerable<Evenement>> GetEvenementsByDate(DateTime date);
        Task<IEnumerable<Evenement>> GetEvenementsByLieu(int lieuId);
    }
}

using BilletterieAPI.Models;

namespace BilletterieAPI.Repositories
{
    public interface IAchatRepository
    {
        Task<IEnumerable<Achat>> GetAchats();
        Task<Achat> GetAchat(int achatId);
        Task<Achat> AddAchat(Achat achat);
        Task<Achat> UpdateAchat(Achat achat);
        Task<Achat> DeleteAchat(int achatId);
    }
}
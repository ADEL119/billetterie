using BilletterieAPI.Models;

namespace BilletterieAPI.Repositories
{
    public interface ISpectateurRepository
    {
        Task<IEnumerable<Spectateur>> GetSpectateurs();
        Task<Spectateur> GetSpectateur(int spectateurId);
        Task<Spectateur> AddSpectateur(Spectateur spectateur);
        Task<Spectateur> UpdateSpectateur(Spectateur spectateur);
        Task<Spectateur> DeleteSpectateur(int spectateurId);
    }
}

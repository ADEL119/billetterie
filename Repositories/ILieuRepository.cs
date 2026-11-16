using BilletterieAPI.Models;

namespace BilletterieAPI.Repositories
{
    public interface ILieuRepository
    {
        Task<IEnumerable<Lieu>> GetLieux();
        Task<Lieu> GetLieu(int lieuId);
        Task<Lieu> AddLieu(Lieu lieu);
        Task<Lieu> UpdateLieu(Lieu lieu);
        Task<Lieu> DeleteLieu(int lieuId);
    }
}

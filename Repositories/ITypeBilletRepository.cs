using BilletterieAPI.Models;

namespace BilletterieAPI.Repositories
{
    public interface ITypeBilletRepository
    {
        Task<IEnumerable<TypeBillet>> GetTypeBillets();
        Task<TypeBillet> GetTypeBillet(int typeBilletId);
        Task<TypeBillet> AddTypeBillet(TypeBillet typeBillet);
        Task<TypeBillet> UpdateTypeBillet(TypeBillet typeBillet);
        Task<TypeBillet> DeleteTypeBillet(int typeBilletId);
    }
}
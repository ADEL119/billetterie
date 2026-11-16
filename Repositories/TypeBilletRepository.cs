using BilletterieAPI.Data;
using BilletterieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BilletterieAPI.Repositories
{
    public class TypeBilletRepository : ITypeBilletRepository
    {
        private readonly AppDbContext _context;

        public TypeBilletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeBillet>> GetTypeBillets() =>
            await _context.TypeBillets
                .Include(tb => tb.Evenement)
                .ToListAsync();

        public async Task<TypeBillet> GetTypeBillet(int typeBilletId) =>
            await _context.TypeBillets
                .Include(tb => tb.Evenement)
                .FirstOrDefaultAsync(tb => tb.TypeBilletId == typeBilletId);

        public async Task<TypeBillet> AddTypeBillet(TypeBillet typeBillet)
        {
            _context.TypeBillets.Add(typeBillet);
            await _context.SaveChangesAsync();
            return typeBillet;
        }

        public async Task<TypeBillet> UpdateTypeBillet(TypeBillet typeBillet)
        {
            _context.TypeBillets.Update(typeBillet);
            await _context.SaveChangesAsync();
            return typeBillet;
        }

        public async Task<TypeBillet> DeleteTypeBillet(int typeBilletId)
        {
            var typeBillet = await _context.TypeBillets.FindAsync(typeBilletId);
            if (typeBillet != null)
            {
                _context.TypeBillets.Remove(typeBillet);
                await _context.SaveChangesAsync();
            }
            return typeBillet;
        }
    }
}
using BilletterieAPI.Data;
using BilletterieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BilletterieAPI.Repositories
{
    public class AchatRepository : IAchatRepository
    {
        private readonly AppDbContext _context;

        public AchatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Achat>> GetAchats() =>
            await _context.Achats
                .Include(a => a.TypeBillet)
                .Include(a => a.Spectateur)
                .ToListAsync();

        public async Task<Achat> GetAchat(int achatId) =>
            await _context.Achats
                .Include(a => a.TypeBillet)
                .Include(a => a.Spectateur)
                .FirstOrDefaultAsync(a => a.AchatId == achatId);

        public async Task<Achat> AddAchat(Achat achat)
        {
            _context.Achats.Add(achat);
            await _context.SaveChangesAsync();
            return achat;
        }

        public async Task<Achat> UpdateAchat(Achat achat)
        {
            _context.Achats.Update(achat);
            await _context.SaveChangesAsync();
            return achat;
        }

        public async Task<Achat> DeleteAchat(int achatId)
        {
            var achat = await _context.Achats.FindAsync(achatId);
            if (achat != null)
            {
                _context.Achats.Remove(achat);
                await _context.SaveChangesAsync();
            }
            return achat;
        }
    }
}
using BilletterieAPI.Data;
using BilletterieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BilletterieAPI.Repositories
{
    public class LieuRepository : ILieuRepository
    {
        private readonly AppDbContext _context;

        public LieuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lieu>> GetLieux() =>
            await _context.Lieux.ToListAsync();

        public async Task<Lieu> GetLieu(int lieuId) =>
            await _context.Lieux.FindAsync(lieuId);

        public async Task<Lieu> AddLieu(Lieu lieu)
        {
            _context.Lieux.Add(lieu);
            await _context.SaveChangesAsync();
            return lieu;
        }

        public async Task<Lieu> UpdateLieu(Lieu lieu)
        {
            _context.Lieux.Update(lieu);
            await _context.SaveChangesAsync();
            return lieu;
        }

        public async Task<Lieu> DeleteLieu(int lieuId)
        {
            var lieu = await _context.Lieux.FindAsync(lieuId);
            if (lieu != null)
            {
                _context.Lieux.Remove(lieu);
                await _context.SaveChangesAsync();
            }
            return lieu;
        }
    }
}

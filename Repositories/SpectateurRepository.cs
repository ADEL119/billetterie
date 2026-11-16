using BilletterieAPI.Data;
using BilletterieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BilletterieAPI.Repositories
{
    public class SpectateurRepository : ISpectateurRepository
    {
        private readonly AppDbContext _context;

        public SpectateurRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Spectateur>> GetSpectateurs() =>
            await _context.Spectateurs.ToListAsync();

        public async Task<Spectateur> GetSpectateur(int spectateurId) =>
            await _context.Spectateurs.FindAsync(spectateurId);

        public async Task<Spectateur> AddSpectateur(Spectateur spectateur)
        {
            _context.Spectateurs.Add(spectateur);
            await _context.SaveChangesAsync();
            return spectateur;
        }

        public async Task<Spectateur> UpdateSpectateur(Spectateur spectateur)
        {
            _context.Spectateurs.Update(spectateur);
            await _context.SaveChangesAsync();
            return spectateur;
        }

        public async Task<Spectateur> DeleteSpectateur(int spectateurId)
        {
            var spectateur = await _context.Spectateurs.FindAsync(spectateurId);
            if (spectateur != null)
            {
                _context.Spectateurs.Remove(spectateur);
                await _context.SaveChangesAsync();
            }
            return spectateur;
        }
    }
}

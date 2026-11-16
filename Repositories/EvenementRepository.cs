using BilletterieAPI.Data;
using BilletterieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BilletterieAPI.Repositories
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly AppDbContext _context;

        public EvenementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evenement>> GetEvenements() =>
            await _context.Evenements
                .Include(e => e.Lieu)
                .ToListAsync();

        public async Task<Evenement> GetEvenement(int evenementId) =>
            await _context.Evenements
                .Include(e => e.Lieu)
                .FirstOrDefaultAsync(e => e.EvenementId == evenementId);

        public async Task<Evenement> AddEvenement(Evenement evenement)
        {
            _context.Evenements.Add(evenement);
            await _context.SaveChangesAsync();
            return evenement;
        }

        public async Task<Evenement> UpdateEvenement(Evenement evenement)
        {
            _context.Evenements.Update(evenement);
            await _context.SaveChangesAsync();
            return evenement;
        }

        public async Task<Evenement> DeleteEvenement(int evenementId)
        {
            var evenement = await _context.Evenements.FindAsync(evenementId);
            if (evenement != null)
            {
                _context.Evenements.Remove(evenement);
                await _context.SaveChangesAsync();
            }
            return evenement;
        }
    }
}

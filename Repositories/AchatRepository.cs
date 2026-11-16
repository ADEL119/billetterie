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
            // Récupérer le type de billet associé
            var typeBillet = await _context.TypeBillets.FindAsync(achat.TypeBilletId);
            
            // Vérifier si le type existe et si le stock est suffisant pour la quantité demandée
            if (typeBillet == null || typeBillet.QuantiteStock < achat.QuantiteAchetee)
            {
                return null; // Stock insuffisant ou type de billet introuvable
            }

            // Décrémenter le stock de la quantité achetée
            typeBillet.QuantiteStock -= achat.QuantiteAchetee;
            _context.TypeBillets.Update(typeBillet);

            // Ajouter l'achat
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
            var achat = await _context.Achats
                .Include(a => a.TypeBillet)
                .FirstOrDefaultAsync(a => a.AchatId == achatId);
                
            if (achat != null)
            {
                // Remettre les billets en stock en fonction de la quantité qui avait été achetée
                if (achat.TypeBillet != null)
                {
                    achat.TypeBillet.QuantiteStock += achat.QuantiteAchetee;
                }

                _context.Achats.Remove(achat);
                await _context.SaveChangesAsync();
            }
            return achat;
        }
    }
}
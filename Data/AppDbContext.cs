using BilletterieAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BilletterieAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Lieu> Lieux { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<TypeBillet> TypeBillets { get; set; }
        public DbSet<Spectateur> Spectateurs { get; set; }
        public DbSet<Achat> Achats { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Précision pour le type decimal (Tarif)
            builder.Entity<TypeBillet>()
                .Property(t => t.Tarif)
                .HasColumnType("decimal(18,2)");
        }
    }
}
using BilletterieAPI.Data;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace BilletterieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Repositories
            builder.Services.AddScoped<IAchatRepository, AchatRepository>();
            builder.Services.AddScoped<ITypeBilletRepository, TypeBilletRepository>();
            builder.Services.AddScoped<IEvenementRepository, EvenementRepository>();
            builder.Services.AddScoped<ILieuRepository, LieuRepository>();
            builder.Services.AddScoped<ISpectateurRepository, SpectateurRepository>();

            // Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Connexion SQL Server
            var cnx = builder.Configuration.GetConnectionString("myConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'myConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(cnx));

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // HTTP pipeline
            app.UseHttpsRedirection();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
 .AddEntityFrameworkStores<AppDbContext>();
app.UseAuthentication();
app.UseAuthorization();

           

            app.MapControllers();

            app.Run();
        }
    }
}
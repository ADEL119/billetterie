
using BilletterieAPI.Data;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
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

            // Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Connexion SQL Server
            var cnx = builder.Configuration.GetConnectionString("myConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'myConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(cnx));

            // AutoMapper (si nécessaire)
             builder.Services.AddAutoMapper(typeof(MappingProfile));

          

            // Swagger avec configuration du token JWT
           var app = builder.Build();
            // HTTP pipeline
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

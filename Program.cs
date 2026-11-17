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
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Repositories
            builder.Services.AddScoped<IAchatRepository, AchatRepository>();
            builder.Services.AddScoped<ITypeBilletRepository, TypeBilletRepository>();
            builder.Services.AddScoped<IEvenementRepository, EvenementRepository>();
            builder.Services.AddScoped<ILieuRepository, LieuRepository>();
            builder.Services.AddScoped<ISpectateurRepository, SpectateurRepository>();

            // Controllers & API Explorer
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Connexion SQL Server
            var cnx = builder.Configuration.GetConnectionString("myConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'myConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(cnx));

            // 1. Configuration d'Identity (Comptes et Rôles)
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // 2. Configuration de l'authentification JWT (Bearer)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // 3. Configuration de Swagger avec le bouton Authorize (Critère T7)
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Entrez votre token JWT sous la forme : Bearer {votre_token}"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
                });
            });

            var app = builder.Build();

            // HTTP pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = string.Empty; // Ouvre Swagger directement à la racine
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BilletterieAPI v1");
                });
            }

            app.UseHttpsRedirection();

            // 4. Ordre crucial du middleware de sécurité
            app.UseAuthentication(); // D'abord on identifie l'utilisateur via le token
            app.UseAuthorization();  // Ensuite on vérifie ses autorisations

            app.MapControllers();
            
            // ==========================================
            // SEEDING DES RÔLES ET DE L'ADMIN PAR DÉFAUT
            // ==========================================
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    // 1. Créer les rôles par défaut s'ils n'existent pas
                    string[] roleNames = { "Admin", "Spectateur" };
                    foreach (var roleName in roleNames)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                    // 2. Créer un Admin par défaut s'il n'existe pas
                    var adminEmail = "adelselmi@iit.tn";
                    var adminUser = await userManager.FindByEmailAsync(adminEmail);
                    if (adminUser == null)
                    {
                        var newAdmin = new ApplicationUser
                        {
                            UserName = adminEmail,
                            Email = adminEmail,
                            EmailConfirmed = true
                        };
                        var createAdmin = await userManager.CreateAsync(newAdmin, "rewhspb25"); // Mot de passe par défaut
                        if (createAdmin.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newAdmin, "Admin");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gérer l'erreur si nécessaire
                }
            }
            // ==========================================

            app.Run();
        }
    }
}
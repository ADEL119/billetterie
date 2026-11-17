using BilletterieAPI.Data;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BilletterieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, AppDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        // 1. INSCRIPTION : Crée le compte de sécurité ET le profil du spectateur en même temps
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Étape A : Création de l'utilisateur technique (Identity) avec hachage automatique du mot de passe
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Étape B : Création du profil métier (Spectateur) lié grâce au user.Id
            var spectateur = new Spectateur
            {
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Email = dto.Email,
            };

            _context.Spectateurs.Add(spectateur);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inscription réussie : Compte et profil spectateur créés !" });
        }

        // 2. CONNEXION : Vérifie les identifiants, récupère les rôles et génère un jeton JWT sécurisé
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            // Recherche de l'utilisateur par son nom (email ici)
            var user = await _userManager.FindByEmailAsync(dto.Username);

            // Vérification si l'utilisateur existe et si le mot de passe est correct
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized(new { message = "Identifiants incorrects." });
            }

            // -------------------------------------------------------------
            // NOUVEAU : Récupération des rôles assignés à l'utilisateur
            // -------------------------------------------------------------
            var userRoles = await _userManager.GetRolesAsync(user);

            // Définition des "Claims" (les informations stockées à l'intérieur du jeton JWT)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id), // L'ID technique
                new Claim(ClaimTypes.Name, user.UserName),      // Le nom d'utilisateur
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Ajout de chaque rôle de l'utilisateur dans les claims du token
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Récupération de la clé secrète depuis appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Création du Token JWT valide pendant 1 heure contenant les rôles
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            // On retourne le token généré au client
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
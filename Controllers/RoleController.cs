using BilletterieAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // 1. Créer un rôle (ex: Admin, Spectateur)
    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest("Le nom du rôle ne peut pas être vide.");

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return Ok(new { message = $"Rôle '{roleName}' créé avec succès." });

            return BadRequest(result.Errors);
        }
        return BadRequest("Ce rôle existe déjà.");
    }

    // 2. Lister tous les rôles
    [HttpGet("list")]
    public async Task<IActionResult> ListRoles()
    {
        var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        return Ok(roles);
    }

    // 3. Attribuer un rôle à un utilisateur
    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(string username, string roleName)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return NotFound("Utilisateur introuvable.");

        if (!await _roleManager.RoleExistsAsync(roleName))
            return NotFound("Rôle introuvable.");

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
            return Ok(new { message = $"Rôle '{roleName}' attribué à '{username}' avec succès." });

        return BadRequest(result.Errors);
    }

    // 4. Retirer un rôle d'un utilisateur
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole(string username, string roleName)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return NotFound("Utilisateur introuvable.");

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (result.Succeeded)
            return Ok(new { message = $"Rôle '{roleName}' retiré de '{username}' avec succès." });

        return BadRequest(result.Errors);
    }
}
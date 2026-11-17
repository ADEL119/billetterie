using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{   
    [Authorize] // Nécessite d'être authentifié par défaut pour tout le contrôleur
    [Route("api/[controller]")]
    [ApiController]
    public class AchatController : ControllerBase
    {
        private readonly IAchatRepository _achatRepository;
        private readonly IMapper _mapper;

        public AchatController(IAchatRepository achatRepository, IMapper mapper)
        {
            _achatRepository = achatRepository;
            _mapper = mapper;
        }

        // Lecture de tous les achats : Réservé aux Admins (ou Admin/Spectateur selon vos préférences)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListAchats()
        {
            var achats = await _achatRepository.GetAchats();
            return Ok(_mapper.Map<IEnumerable<AchatDTO>>(achats));
        }

        // Lecture d'un achat par son ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<ActionResult<AchatDTO>> GetAchatById(int id)
        {
            var achat = await _achatRepository.GetAchat(id);
            if (achat == null) return NotFound();
            return Ok(_mapper.Map<AchatDTO>(achat));
        }

        // Créer un achat : Un spectateur peut acheter des billets (ainsi que l'admin)
        [HttpPost]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<ActionResult<AchatDTO>> AjouterAchat([FromBody] AchatDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var achat = _mapper.Map<Achat>(dto);
            var result = await _achatRepository.AddAchat(achat);
            
            // Si le résultat est null, c'est que le stock est insuffisant ou le type invalide
            if (result == null)
            {
                return BadRequest(new { message = "Achat refusé : stock insuffisant, quantité demandée trop élevée ou type de billet invalide." });
            }

            return Ok(_mapper.Map<AchatDTO>(result));
        }

        // Modifier un achat : Action sensible réservée à l'administrateur
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AchatDTO>> ModifierAchat(int id, [FromBody] AchatDTO dto)
        {
            var achat = _mapper.Map<Achat>(dto);
            achat.AchatId = id;
            var result = await _achatRepository.UpdateAchat(achat);
            return Ok(_mapper.Map<AchatDTO>(result));
        }

        // Supprimer un achat : Action critique réservée strictement à l'administrateur
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SupprimerAchat(int id)
        {
            var result = await _achatRepository.DeleteAchat(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
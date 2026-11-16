using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> ListAchats()
        {
            var achats = await _achatRepository.GetAchats();
            return Ok(_mapper.Map<IEnumerable<AchatDTO>>(achats));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AchatDTO>> GetAchatById(int id)
        {
            var achat = await _achatRepository.GetAchat(id);
            if (achat == null) return NotFound();
            return Ok(_mapper.Map<AchatDTO>(achat));
        }

        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<ActionResult<AchatDTO>> ModifierAchat(int id, [FromBody] AchatDTO dto)
        {
            var achat = _mapper.Map<Achat>(dto);
            achat.AchatId = id;
            var result = await _achatRepository.UpdateAchat(achat);
            return Ok(_mapper.Map<AchatDTO>(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SupprimerAchat(int id)
        {
            var result = await _achatRepository.DeleteAchat(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{   
    [Authorize] // Sécurise par défaut l'ensemble du contrôleur
    [Route("api/[controller]")]
    [ApiController]
    public class SpectateurController : ControllerBase
    {
        private readonly ISpectateurRepository _spectateurRepository;
        private readonly IMapper _mapper;

        public SpectateurController(ISpectateurRepository spectateurRepository, IMapper mapper)
        {
            _spectateurRepository = spectateurRepository;
            _mapper = mapper;
        }

        // Lister les spectateurs : Réservé à l'administrateur
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListSpectateurs()
        {
            var spectateurs = await _spectateurRepository.GetSpectateurs();
            return Ok(_mapper.Map<IEnumerable<SpectateurDTO>>(spectateurs));
        }

        // Obtenir un spectateur par ID : Accessible à l'admin et au spectateur
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<ActionResult<SpectateurDTO>> GetSpectateurById(int id)
        {
            var spectateur = await _spectateurRepository.GetSpectateur(id);
            if (spectateur == null) return NotFound();
            return Ok(_mapper.Map<SpectateurDTO>(spectateur));
        }

        // Ajouter un spectateur : Réservé à l'administrateur
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SpectateurDTO>> AjouterSpectateur([FromBody] SpectateurDTO dto)
        {
            var spectateur = _mapper.Map<Spectateur>(dto);
            var result = await _spectateurRepository.AddSpectateur(spectateur);
            return Ok(_mapper.Map<SpectateurDTO>(result));
        }

        // Modifier un spectateur : Accessible à l'admin et au spectateur (pour son propre profil)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<ActionResult<SpectateurDTO>> ModifierSpectateur(int id, [FromBody] SpectateurDTO dto)
        {
            var spectateur = _mapper.Map<Spectateur>(dto);
            spectateur.SpectateurId = id;
            var result = await _spectateurRepository.UpdateSpectateur(spectateur);
            return Ok(_mapper.Map<SpectateurDTO>(result));
        }

        // Supprimer un spectateur : Réservé strictement à l'administrateur
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SupprimerSpectateur(int id)
        {
            var result = await _spectateurRepository.DeleteSpectateur(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
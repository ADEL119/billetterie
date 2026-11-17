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
    public class LieuController : ControllerBase
    {
        private readonly ILieuRepository _lieuRepository;
        private readonly IMapper _mapper;

        public LieuController(ILieuRepository lieuRepository, IMapper mapper)
        {
            _lieuRepository = lieuRepository;
            _mapper = mapper;
        }

        // Lister les lieux : Accessible aux spectateurs et aux administrateurs
        [HttpGet]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<IActionResult> ListLieux()
        {
            var lieux = await _lieuRepository.GetLieux();
            return Ok(_mapper.Map<IEnumerable<LieuDTO>>(lieux));
        }

        // Obtenir un lieu par ID : Accessible aux spectateurs et aux administrateurs
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<ActionResult<LieuDTO>> GetLieuById(int id)
        {
            var lieu = await _lieuRepository.GetLieu(id);
            if (lieu == null) return NotFound();
            return Ok(_mapper.Map<LieuDTO>(lieu));
        }

        // Ajouter un lieu : Réservé strictement à l'administrateur
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LieuDTO>> AjouterLieu([FromBody] LieuDTO dto)
        {
            var lieu = _mapper.Map<Lieu>(dto);
            var result = await _lieuRepository.AddLieu(lieu);
            return Ok(_mapper.Map<LieuDTO>(result));
        }

        // Modifier un lieu : Réservé strictement à l'administrateur
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LieuDTO>> ModifierLieu(int id, [FromBody] LieuDTO dto)
        {
            var lieu = _mapper.Map<Lieu>(dto);
            lieu.LieuId = id;
            var result = await _lieuRepository.UpdateLieu(lieu);
            return Ok(_mapper.Map<LieuDTO>(result));
        }

        // Supprimer un lieu : Réservé strictement à l'administrateur
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SupprimerLieu(int id)
        {
            var result = await _lieuRepository.DeleteLieu(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
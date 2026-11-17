using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{   
    [Authorize] // Sécurise par défaut tout le contrôleur
    [Route("api/[controller]")]
    [ApiController]
    public class EvenementController : ControllerBase
    {
        private readonly IEvenementRepository _evenementRepository;
        private readonly IMapper _mapper;

        public EvenementController(IEvenementRepository evenementRepository, IMapper mapper)
        {
            _evenementRepository = evenementRepository;
            _mapper = mapper;
        }

        // Lister les événements : Accessible aux spectateurs et aux admins
        [HttpGet]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<IActionResult> ListEvenements()
        {
            var evenements = await _evenementRepository.GetEvenements();
            return Ok(_mapper.Map<IEnumerable<EvenementDTO>>(evenements));
        }

        // Obtenir un événement par ID : Accessible aux spectateurs et aux admins
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<ActionResult<EvenementDTO>> GetEvenementById(int id)
        {
            var evenement = await _evenementRepository.GetEvenement(id);
            if (evenement == null) return NotFound();
            return Ok(_mapper.Map<EvenementDTO>(evenement));
        }

        // Ajouter un événement : Réservé strictement à l'administrateur
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EvenementDTO>> AjouterEvenement([FromBody] EvenementDTO dto)
        {
            var evenement = _mapper.Map<Evenement>(dto);
            var result = await _evenementRepository.AddEvenement(evenement);
            return Ok(_mapper.Map<EvenementDTO>(result));
        }

        // Modifier un événement : Réservé strictement à l'administrateur
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EvenementDTO>> ModifierEvenement(int id, [FromBody] EvenementDTO dto)
        {
            var evenement = _mapper.Map<Evenement>(dto);
            evenement.EvenementId = id;
            var result = await _evenementRepository.UpdateEvenement(evenement);
            return Ok(_mapper.Map<EvenementDTO>(result));
        }

        // Supprimer un événement : Réservé strictement à l'administrateur
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SupprimerEvenement(int id)
        {
            var result = await _evenementRepository.DeleteEvenement(id);
            if (result == null) return NotFound();
            return NoContent();
        }

        // Recherche par date : Accessible aux spectateurs et aux admins
        [HttpGet("date/{date}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<IActionResult> GetEvenementsByDate(DateTime date)
        {
            var evenements = await _evenementRepository.GetEvenementsByDate(date);
            return Ok(_mapper.Map<IEnumerable<EvenementDTO>>(evenements));
        }

        // Recherche par lieu : Accessible aux spectateurs et aux admins
        [HttpGet("lieu/{lieuId}")]
        [Authorize(Roles = "Admin,Spectateur")]
        public async Task<IActionResult> GetEvenementsByLieu(int lieuId)
        {
            var evenements = await _evenementRepository.GetEvenementsByLieu(lieuId);
            return Ok(_mapper.Map<IEnumerable<EvenementDTO>>(evenements));
        }
    }
}
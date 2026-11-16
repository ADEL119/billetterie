using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> ListEvenements()
        {
            var evenements = await _evenementRepository.GetEvenements();
            return Ok(_mapper.Map<IEnumerable<EvenementDTO>>(evenements));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EvenementDTO>> GetEvenementById(int id)
        {
            var evenement = await _evenementRepository.GetEvenement(id);
            if (evenement == null) return NotFound();
            return Ok(_mapper.Map<EvenementDTO>(evenement));
        }

        [HttpPost]
        public async Task<ActionResult<EvenementDTO>> AjouterEvenement([FromBody] EvenementDTO dto)
        {
            var evenement = _mapper.Map<Evenement>(dto);
            var result = await _evenementRepository.AddEvenement(evenement);
            return Ok(_mapper.Map<EvenementDTO>(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EvenementDTO>> ModifierEvenement(int id, [FromBody] EvenementDTO dto)
        {
            var evenement = _mapper.Map<Evenement>(dto);
            evenement.EvenementId = id;
            var result = await _evenementRepository.UpdateEvenement(evenement);
            return Ok(_mapper.Map<EvenementDTO>(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SupprimerEvenement(int id)
        {
            var result = await _evenementRepository.DeleteEvenement(id);
            if (result == null) return NotFound();
            return NoContent();
        }
        // Endpoint de recherche par date (ex: /api/Evenement/date/2026-06-15)
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetEvenementsByDate(DateTime date)
        {
            var evenements = await _evenementRepository.GetEvenementsByDate(date);
            return Ok(_mapper.Map<IEnumerable<EvenementDTO>>(evenements));
        }

        // Endpoint de recherche par lieu (ex: /api/Evenement/lieu/1)
        [HttpGet("lieu/{lieuId}")]
        public async Task<IActionResult> GetEvenementsByLieu(int lieuId)
        {
            var evenements = await _evenementRepository.GetEvenementsByLieu(lieuId);
            return Ok(_mapper.Map<IEnumerable<EvenementDTO>>(evenements));
        }
    }
}

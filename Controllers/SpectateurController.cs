using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> ListSpectateurs()
        {
            var spectateurs = await _spectateurRepository.GetSpectateurs();
            return Ok(_mapper.Map<IEnumerable<SpectateurDTO>>(spectateurs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpectateurDTO>> GetSpectateurById(int id)
        {
            var spectateur = await _spectateurRepository.GetSpectateur(id);
            if (spectateur == null) return NotFound();
            return Ok(_mapper.Map<SpectateurDTO>(spectateur));
        }

        [HttpPost]
        public async Task<ActionResult<SpectateurDTO>> AjouterSpectateur([FromBody] SpectateurDTO dto)
        {
            var spectateur = _mapper.Map<Spectateur>(dto);
            var result = await _spectateurRepository.AddSpectateur(spectateur);
            return Ok(_mapper.Map<SpectateurDTO>(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SpectateurDTO>> ModifierSpectateur(int id, [FromBody] SpectateurDTO dto)
        {
            var spectateur = _mapper.Map<Spectateur>(dto);
            spectateur.SpectateurId = id;
            var result = await _spectateurRepository.UpdateSpectateur(spectateur);
            return Ok(_mapper.Map<SpectateurDTO>(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SupprimerSpectateur(int id)
        {
            var result = await _spectateurRepository.DeleteSpectateur(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}

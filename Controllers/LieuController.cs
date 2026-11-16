using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> ListLieux()
        {
            var lieux = await _lieuRepository.GetLieux();
            return Ok(_mapper.Map<IEnumerable<LieuDTO>>(lieux));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LieuDTO>> GetLieuById(int id)
        {
            var lieu = await _lieuRepository.GetLieu(id);
            if (lieu == null) return NotFound();
            return Ok(_mapper.Map<LieuDTO>(lieu));
        }

        [HttpPost]
        public async Task<ActionResult<LieuDTO>> AjouterLieu([FromBody] LieuDTO dto)
        {
            var lieu = _mapper.Map<Lieu>(dto);
            var result = await _lieuRepository.AddLieu(lieu);
            return Ok(_mapper.Map<LieuDTO>(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LieuDTO>> ModifierLieu(int id, [FromBody] LieuDTO dto)
        {
            var lieu = _mapper.Map<Lieu>(dto);
            lieu.LieuId = id;
            var result = await _lieuRepository.UpdateLieu(lieu);
            return Ok(_mapper.Map<LieuDTO>(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SupprimerLieu(int id)
        {
            var result = await _lieuRepository.DeleteLieu(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}

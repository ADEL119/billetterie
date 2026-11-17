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
    public class TypeBilletController : ControllerBase
    {
        private readonly ITypeBilletRepository _typeBilletRepository;
        private readonly IMapper _mapper;

        public TypeBilletController(ITypeBilletRepository typeBilletRepository, IMapper mapper)
        {
            _typeBilletRepository = typeBilletRepository;
            _mapper = mapper;
        }

        // Ajouter un type de billet : Réservé strictement à l'administrateur
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TypeBilletDTO>> AjouterTypeBillet([FromBody] TypeBilletDTO dto)
        {
            var typeBillet = _mapper.Map<TypeBillet>(dto);
            var result = await _typeBilletRepository.AddTypeBillet(typeBillet);
            return Ok(_mapper.Map<TypeBilletDTO>(result));
        }
    }
}
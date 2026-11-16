using AutoMapper;
using BilletterieAPI.DTOs;
using BilletterieAPI.Models;
using BilletterieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BilletterieAPI.Controllers
{
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

    

        [HttpPost]
        public async Task<ActionResult<TypeBilletDTO>> AjouterTypeBillet([FromBody] TypeBilletDTO dto)
        {
            var typeBillet = _mapper.Map<TypeBillet>(dto);
            var result = await _typeBilletRepository.AddTypeBillet(typeBillet);
            return Ok(_mapper.Map<TypeBilletDTO>(result));
        }

       
    }
}

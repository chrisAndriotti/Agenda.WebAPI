using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaWebAPI.Data;
using AgendaWebAPI.Dtos;
using AgendaWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgendaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        public readonly IRepository _repo;

        private readonly IMapper _mapper;

        public ContatoController(IRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        // api/contato     [Retorna todos os contatos cadastrados]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contatos = await _repo.GetAllContatos(false);

            if (contatos == null) return BadRequest("Não há contatos cadastrados!");

            var contatosMapped = _mapper.Map<IEnumerable<ContatoDto>>(contatos);
            
            return Ok(contatosMapped);
        }

        // api/contato/id     [Retorna um contato pelo ID]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contato = await _repo.GetContatoByIdAsync(id, false);

            if (contato == null) return BadRequest("Contato não encontrado!");

            var contatoMapped = _mapper.Map<ContatoDto>(contato);

            return Ok(contatoMapped);
        }

        // api/contato/byEvento/id     [Retorna todos os contatos pelo ID de um evento]
        [HttpGet("byEvento/{id}")]
        public async Task<IActionResult> GetByEventoId(int id)
        {
            var contatos = await _repo.GetAllContatosByEventoId(id);

            if (contatos == null) return BadRequest("Não há contatos no evento escolhido!");

            var contatosMapped = _mapper.Map<IEnumerable<ContatoDto>>(contatos);

            return Ok(contatosMapped);

        }

        // api/contato/
        [HttpPost]
        public IActionResult Post(ContatoRegistrarDto model)
        {
            var contato = _mapper.Map<Contato>(model);

            _repo.Add(contato);

            if (_repo.SaveChanges())
            {
                return Created($"/api/contato/{contato.Id}", _mapper.Map<ContatoDto>(contato));
            }

            return BadRequest("Não foi possível cadastrar!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ContatoRegistrarDto model)
        {
            var contato = _repo.GetContatoById(id, false);

            if (contato == null) return BadRequest("Contato não encontrado!");
        
            _mapper.Map(model, contato);

            _repo.Update(contato);

            if (_repo.SaveChanges())
            {
                return Created($"/api/contato/{model.Id}", _mapper.Map<ContatoDto>(contato));
            }

            return BadRequest("Contato não atualizado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contato = _repo.GetContatoById(id, false);
            if (contato == null) return BadRequest("Contato não encontrado!");
            
            _repo.Delete(contato);
            if (_repo.SaveChanges())
            {
                return Ok("Contato removido!");
            }

            return BadRequest("Contato não removido!");
        }
    }
}
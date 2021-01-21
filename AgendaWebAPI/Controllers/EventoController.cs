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
    public class EventoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public EventoController(IRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        // api/evento     [Retorna todos os eventos cadastrados]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventos = await _repo.GetAllEventos(false);

            if (eventos == null) return BadRequest("Não há eventos cadastrados!");

            var eventosMapper = _mapper.Map<IEnumerable<EventoDto>>(eventos);

            return Ok(eventosMapper);
        }

        // api/evento/id     [Retorna um evento pelo ID]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evento = await _repo.GetEventoByIdAsync(id, false);

            if (evento == null) return BadRequest("Evento não encontrado!");
            
            var eventosMapper = _mapper.Map<EventoDto>(evento);

            return Ok(eventosMapper);
        }

        // api/evento/byContato/id     [Retorna todos os eventos pelo ID de um contato]
        [HttpGet("byContato/{id}")]
        public async Task<IActionResult> GetByEventoId(int id)
        {
            var eventos = await _repo.GetAllEventosByContatoId(id);

            if (eventos == null) return BadRequest("Não há eventos para o contato escolhido!");

            var eventosMapper = _mapper.Map<IEnumerable<EventoDto>>(eventos);

            return Ok(eventosMapper);

        }

        // api/evento/
        [HttpPost]
        public IActionResult Post(EventoDto model)
        {
            var eventoMapped = _mapper.Map<Evento>(model);

            _repo.Add(eventoMapped);

            if (_repo.SaveChanges())
            {
                return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(eventoMapped));
            }

            return BadRequest("Não foi possível cadastrar!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, EventoDto model)
        {
            var eventos = _repo.GetEventoById(id, false);

            if (eventos == null) return BadRequest("evento não encontrado!");
        
            _mapper.Map(model, eventos);
            _repo.Update(eventos);

            if (_repo.SaveChanges())
            {
                 return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(eventos));
            }

            return BadRequest("Evento não atualizado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var evento = _repo.GetEventoById(id, false);
            if (evento == null) return BadRequest("evento não encontrado!");
            
            _repo.Delete(evento);
            if (_repo.SaveChanges())
            {
                return Ok("Evento removido!");
            }

            return BadRequest("Evento não removido!");
        }
    }
}
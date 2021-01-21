using System.Linq;
using System.Threading.Tasks;
using AgendaWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly AgendaContext _context;
        public Repository(AgendaContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public bool SaveChanges()
        {
            return(_context.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Contato[]> GetAllContatos(bool includeEvento = false)
        {
            IQueryable<Contato> query = _context.Contatos;

            if (includeEvento)
            {
                query = query.Include(pe => pe.PessoasEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking().OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Contato> GetContatoByIdAsync(int contatoId, bool includeEvento = false)
        {
            IQueryable<Contato> query = _context.Contatos;

            if (includeEvento)
            {
                query = query.Include(pe => pe.PessoasEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                        .Where(c => c.Id == contatoId);

            return await query.FirstOrDefaultAsync();
            
        }

        public Contato GetContatoById(int contatoId, bool includeEvento = false)
        {
            IQueryable<Contato> query = _context.Contatos;

            if (includeEvento)
            {
                query = query.Include(pe => pe.PessoasEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                        .Where(c => c.Id == contatoId);

            return query.FirstOrDefault();
        }

        public async Task<Contato[]> GetAllContatosByEventoId(int eventoId)
        {
            IQueryable<Contato> query = _context.Contatos;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(pe => pe.PessoasEventos.Any(e => e.EventoId == eventoId));
            
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventos(bool includeContato = false)
        {
            IQueryable<Evento> query = _context.Eventos;

            if (includeContato)
            {
                query = query.Include(pe => pe.PessoasEventos)
                            .ThenInclude(c => c.Contato);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoById(int eventoId, bool includeContato = false)
        {
            IQueryable<Evento> query = _context.Eventos;

            if (includeContato)
            {
                query = query.Include(pe => pe.PessoasEventos)
                            .ThenInclude(c => c.Contato);
            }

            query = query.AsNoTracking()
                        .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> GetAllEventosByContatoId(int contatoId)
        {
            IQueryable<Evento> query = _context.Eventos;

            query = query.AsNoTracking()
                        .OrderBy(e => e.Id)
                        .Where(pe => pe.PessoasEventos.Any(c => c.ContatoId == contatoId));
            
            return await query.ToArrayAsync();
        }


    }
}
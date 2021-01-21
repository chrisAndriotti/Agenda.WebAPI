using System;
using System.Collections.Generic;

namespace AgendaWebAPI.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public IEnumerable<PessoaEvento> PessoasEventos { get; set; }

        public Evento() { }

        public Evento(int id, string nome, DateTime data)
        {
            this.Id = id;
            this.Nome = nome;
            this.Data = data;
        }
    }
}
using System.Collections.Generic;

namespace AgendaWebAPI.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<PessoaEvento> PessoasEventos { get; set; }

        public Contato() { }
        public Contato(int id, string nome, string sobrenome, string telefone, string email)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.Email = email;
        }
    }

}
namespace AgendaWebAPI.Models
{
    public class PessoaEvento
    {
        public int ContatoId { get; set; }
        public Contato Contato { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public PessoaEvento() {  }

        public PessoaEvento(int contatoId, int eventoId)
        {
            this.ContatoId = contatoId;
            this.EventoId = eventoId;
        }
    }

}
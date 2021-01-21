using System;

namespace AgendaWebAPI.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
    }
}
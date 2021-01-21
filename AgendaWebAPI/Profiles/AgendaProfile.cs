using AgendaWebAPI.Dtos;
using AgendaWebAPI.Models;
using AutoMapper;

namespace AgendaWebAPI.Profiles
{
    public class AgendaProfile : Profile
    {
        public AgendaProfile()
        {
            CreateMap<Contato, ContatoDto>()
            .ForMember(
                dest => dest.Nome,
                opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
            );

            CreateMap<ContatoDto, Contato>();
            CreateMap<Contato, ContatoRegistrarDto>().ReverseMap();
            CreateMap<Evento, EventoDto>().ReverseMap();
        }
    }
}
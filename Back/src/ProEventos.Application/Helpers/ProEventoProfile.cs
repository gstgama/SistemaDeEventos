using AutoMapper;
using ProEventos.Application.Dto;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers
{
  public class ProEventoProfile : Profile
  {
    public ProEventoProfile()
    {
      CreateMap<Evento, EventoDto>().ReverseMap();
    }
  }
}
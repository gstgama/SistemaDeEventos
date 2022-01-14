using System.Threading.Tasks;
using ProEventos.Application.Dto;

namespace ProEventos.Application.Contracts
{
  public interface IEventoService
  {
    Task<EventoDto> AddEventos(EventoDto model);
    Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
    Task<bool> DeleteEvento(int eventoId);
    Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
    Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
    Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
  }
}
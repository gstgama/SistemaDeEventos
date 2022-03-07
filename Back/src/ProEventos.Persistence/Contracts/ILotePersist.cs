using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contracts
{
  public interface ILotePersist
  {
    /// <param name="eventoId">Primary key da tabela Evento</param>
    /// <param name="id">Primary key da tabela Lote</param>
    /// <returns>Retornara 1 lote</returns>
    Task<Lote> GetLoteByIdsAsync(int eventoId, int id);

    /// <param name="eventoId">Primary key da tabela Eventos</param>
    /// <returns>Retornara uma lista de lotes por eventoId</returns>
    Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

  }
}
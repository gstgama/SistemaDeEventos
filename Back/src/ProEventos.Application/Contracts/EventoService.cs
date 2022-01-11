using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Application.Dto;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application.Contracts
{
  public class EventoService : IEventoService
  {
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;

    public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
    {
      _geralPersist = geralPersist;
      _eventoPersist = eventoPersist;
    }
    public async Task<Evento> AddEventos(Evento model)
    {
      try
      {
        _geralPersist.Add<Evento>(model);
        if (await _geralPersist.SaveChangesAsync())
        {
          return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
        }
        return null;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    public async Task<Evento> UpdateEvento(int eventoId, Evento model)
    {
      try
      {
        var evento = _eventoPersist.GetEventoByIdAsync(eventoId, false);
        if (evento == null) return null;

        model.Id = evento.Result.Id;

        _geralPersist.Update(model);
        if (await _geralPersist.SaveChangesAsync())
        {
          return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
        }
        return null;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }

    public async Task<bool> DeleteEvento(int eventoId)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
        if (evento == null) throw new Exception("Evento para delete não encontrado.");

        _geralPersist.Delete<Evento>(evento);
        return await _geralPersist.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
      try
      {
        var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
        if (eventos == null) return null;

        var eventosRetorno = new List<EventoDto>();

        foreach (var evento in eventos)
        {
          eventosRetorno.Add(new EventoDto()
          {
            Id = evento.Id,
            Local = evento.Local,
            DataEvento = evento.DataEvento.ToString(),
            Tema = evento.Tema,
            QtdPessoas = evento.QtdPessoas,
            ImagemURL = evento.ImagemURL,
            Telefone = evento.Telefone,
            Email = evento.Email
          });
        }

        return eventos;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
      try
      {
        var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
        if (eventos == null) return null;

        return eventos;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
    {
      try
      {
        var eventos = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
        if (eventos == null) return null;

        return eventos;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
  }
}
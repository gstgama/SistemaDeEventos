using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using ProEventos.Application.Dto;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EventosController : ControllerBase
  {
    private readonly IEventoService _eventoService;

    public EventosController(IEventoService eventoService)
    {
      _eventoService = eventoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var eventos = await _eventoService.GetAllEventosAsync(true);
        if (eventos == null) return NoContent();

        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
      }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(int id)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(id, true);
        if (evento == null) return NoContent();

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
      }
    }

    [HttpGet("tema/{tema}")]
    public async Task<IActionResult> GetByTema(string tema)
    {
      try
      {
        var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
        if (evento == null) return NoContent();

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
      try
      {
        var evento = await _eventoService.AddEventos(model);
        if (evento == null) return NoContent();

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar evento. Erro: {ex}");
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
      try
      {
        var evento = await _eventoService.UpdateEvento(id, model);
        if (evento == null) return NoContent();

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {ex}");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(id, true);
        if (evento == null) return NoContent();

        return await _eventoService.DeleteEvento(id) ? Ok("Deletado") : throw new Exception("Ocorreu um problema não específico ao tentar deletar o evento");
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento. Erro: {ex}");
      }
    }

  }
}

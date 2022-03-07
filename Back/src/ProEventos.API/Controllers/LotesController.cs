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
  public class LotesController : ControllerBase
  {
    private readonly ILoteService _eventoService;

    public LotesController(ILoteService loteService)
    {
      _loteService = loteService;
    }

    [HttpGet("{eventoId}")]
    public async Task<IActionResult> Get(int eventoId)
    {
      try
      {
        var eventos = await _eventoService.GetEventoByIdAsync(true);
        if (eventos == null) return NoContent();

        return Ok(eventos);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
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

    [HttpPut("{eventoId }")]
    public async Task<IActionResult> Put(int eventoId, LoteDto[] models)
    {
      try
      {
        var evento = await _eventoService.UpdateEvento(eventoId, models);
        if (evento == null) return NoContent();

        return Ok(evento);
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {ex}");
      }
    }

    [HttpDelete("{eventoId}/{loteId}")]
    public async Task<IActionResult> Delete(int eventoId, int loteId)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(id, true);
        if (evento == null) return NoContent();

        return await _eventoService.DeleteEvento(id)
        ? Ok(new { message = "Deletado" })
        : throw new Exception("Ocorreu um problema não específico ao tentar deletar o evento");
      }
      catch (Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento. Erro: {ex.Message}");
      }
    }

  }
}

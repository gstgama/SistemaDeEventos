using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dto
{
  public class EventoDto
  {
    public int Id { get; set; }
    public string Local { get; set; }
    public string DataEvento { get; set; }

    [Required(ErrorMessage = "O campo {0} é necessário."),
    StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 50 caracteres.")]
    public string Tema { get; set; }
    public int QtdPessoas { get; set; }
    public string ImagemURL { get; set; }
    public string Telefone { get; set; }

    [EmailAddress(ErrorMessage = "Campo {0} inválido.")]
    public string Email { get; set; }
    public IEnumerable<LoteDto> Lotes { get; set; }
    public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
    public IEnumerable<PalestranteDto> Palestrantes { get; set; }
  }
}
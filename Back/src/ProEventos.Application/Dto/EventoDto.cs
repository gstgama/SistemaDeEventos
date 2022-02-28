using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dto
{
  public class EventoDto
  {
    public int Id { get; set; }
    public string Local { get; set; }
    public string DataEvento { get; set; }

    [Display(Name = "tema"),
    Required(ErrorMessage = "O campo {0} é necessário."),
    StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 50 caracteres.")]
    public string Tema { get; set; }

    [Display(Name = "quantidade de pessoas"),
    Required(ErrorMessage = "O campo {0} é necessário"),
    Range(1, 120000, ErrorMessage = "O campo {0} deve estar entre 1 e 120000")]
    public int QtdPessoas { get; set; }

    [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
                      ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp, png).")]
    public string ImagemURL { get; set; }

    [Display(Name = "telefone"),
    Phone(ErrorMessage = "O campo {0} está inválido")]
    public string Telefone { get; set; }

    [Display(Name = "e-mail"),
    Required(ErrorMessage = "O campo {0} é necessário"),
    EmailAddress(ErrorMessage = "É campo {0} está inválido.")]
    public string Email { get; set; }
    public IEnumerable<LoteDto> Lotes { get; set; }
    public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
    public IEnumerable<PalestranteDto> Palestrantes { get; set; }
  }
}
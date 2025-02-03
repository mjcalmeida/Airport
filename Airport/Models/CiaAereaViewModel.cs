using System.ComponentModel.DataAnnotations;

namespace Airport.Models;
public class CiaAereaViewModel
{
    public int CiaId { get; set; }
    [Required(ErrorMessage = "O nome da Compania Aérea é obrigatório")]
    public string? Nome { get; set; }
    [Required]
    public string? ImagemURL { get; set; }
}

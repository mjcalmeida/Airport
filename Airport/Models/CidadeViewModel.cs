using System.ComponentModel.DataAnnotations;

namespace Airport.Models;
public class CidadeViewModel
{
    public int CidadeId { get; set; }
    [Required(ErrorMessage = "O nome da Cidade é obrigatório")]
    public string? Nome { get; set; }    
}

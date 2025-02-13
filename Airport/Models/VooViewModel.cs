using System.ComponentModel.DataAnnotations;

namespace Airport.Models;
public class VooViewModel
{
    public int VooId { get; set; }
    [Required(ErrorMessage = "O Numero do Voo é obrigatório")]
    public string? Numero { get; set; }

    [Required]
    public int Cia { get; set; }
    [Required]
    public int Cidade { get; set; }
}

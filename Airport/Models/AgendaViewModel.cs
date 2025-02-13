using System.ComponentModel.DataAnnotations;

namespace Airport.Models;
public class AgendaViewModel
{
    public int idAgenda { get; set; }
    [Required(ErrorMessage = "O dia da Semana é obrigatório")]
    public int idDiaSemana { get; set; }
    [Required(ErrorMessage = "O número do Voo é obrigatório")]
    public int idVoo { get; set; }
    [Required(ErrorMessage = "O Horário do Voo é obrigatório")]
    public string? Horario { get; set; }
}

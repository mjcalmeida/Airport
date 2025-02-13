using Airport.Models;

namespace Airport.Services;
public interface IAgendaService
{
    Task<IEnumerable<AgendaViewModel>> GetAgenda();
    Task<AgendaViewModel> GetAgendaPorId(int id);
    Task<AgendaViewModel> CriaAgenda(AgendaViewModel agendaVM);
    Task<bool> AtualizaAgenda(int id, AgendaViewModel agendaVM);
    Task<bool> DeletaAgenda(int id);
}

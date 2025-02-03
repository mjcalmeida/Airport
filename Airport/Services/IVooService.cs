using Airport.Models;

namespace Airport.Services;
public interface IVooService
{    
    Task<IEnumerable<VooViewModel>> GetVoos();
    Task<VooViewModel> GetVooPorId(int id);
    Task<VooViewModel> CriaVoo(VooViewModel vooVM);
    Task<bool> AtualizaVoo(int id, VooViewModel vooVM);
    Task<bool> DeletaVoo(int id);
}

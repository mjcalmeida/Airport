using Airport.Models;

namespace Airport.Services
{
    public interface ICiaAereaService
    {
        Task<IEnumerable<CiaAereaViewModel>> GetCiaAerea();
        Task<CiaAereaViewModel> GetCiaAereaPorId(int id);
        Task<CiaAereaViewModel> CriaCiaAerea(CiaAereaViewModel vooVM);
        Task<CiaAereaViewModel> AtualizaCiaAerea(int id, CiaAereaViewModel vooVM);
        Task<CiaAereaViewModel> DeletaCiaAerea(int id);
    }
}
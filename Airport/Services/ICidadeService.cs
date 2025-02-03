using Airport.Models;

namespace Airport.Services;
public interface ICidadeService
{
    Task<IEnumerable<CidadeViewModel>> GetCidades();
    Task<CidadeViewModel> GetCidadePorId(int id);
    Task<CidadeViewModel> CriaCidade(CidadeViewModel cidadeVM);
    Task<bool> AtualizaCidade(int id, CidadeViewModel cidadeVM);
    Task<bool> DeletaCidade(int id);
}

using Airport.Models;
using System.Text;
using System.Text.Json;

namespace Airport.Services;
public class CidadeService : ICidadeService
{
    private const string apiEndpoint = "/api/v1/Cidade";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private CidadeViewModel _cidadeVM = new CidadeViewModel();
    private IEnumerable<CidadeViewModel> _cidadesVM = new List<CidadeViewModel>();

    public CidadeService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<CidadeViewModel>> GetCidades()
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var cidades = await JsonSerializer
                    .DeserializeAsync<IEnumerable<CidadeViewModel>>(apiResponse, _options);
                _cidadesVM = cidades ?? new List<CidadeViewModel>();
            }
            else
            {
                return Enumerable.Empty<CidadeViewModel>();
            }
            return _cidadesVM;
        }
    }

    public async Task<CidadeViewModel> GetCidadePorId(int id)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var cidade = await JsonSerializer
                    .DeserializeAsync<CidadeViewModel>(apiResponse, _options);
                _cidadeVM = cidade ?? new CidadeViewModel();
            }
            else
            {
                return new CidadeViewModel();
            }
            return _cidadeVM;
        }
    }

    public async Task<CidadeViewModel> CriaCidade(CidadeViewModel cidadeVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        var content = new StringContent(
            JsonSerializer.Serialize(cidadeVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var createdCidade = await JsonSerializer
                    .DeserializeAsync<CidadeViewModel>(apiResponse, _options);
                _cidadeVM = createdCidade ?? new CidadeViewModel();
            }
            else
            {
                return new CidadeViewModel();
            }
            return _cidadeVM;
        }
    }

    public async Task<bool> AtualizaCidade(int id, CidadeViewModel cidadeVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, cidadeVM))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public async Task<bool> DeletaCidade(int id)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }
}

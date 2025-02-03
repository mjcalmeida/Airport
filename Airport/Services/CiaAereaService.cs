using Airport.Models;
using System.Text;
using System.Text.Json;

namespace Airport.Services;
public class CiaAereaService : ICiaAereaService
{
    private const string apiEndpoint = "/api/v1/CiasAereas";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private CiaAereaViewModel _ciaaereaVM = new CiaAereaViewModel();
    private IEnumerable<CiaAereaViewModel> _ciaaereasVM = new List<CiaAereaViewModel>();

    public CiaAereaService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<CiaAereaViewModel>> GetCiaAerea()
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var ciaaereas = await JsonSerializer
                    .DeserializeAsync<IEnumerable<CiaAereaViewModel>>(apiResponse, _options);
                _ciaaereasVM = ciaaereas ?? new List<CiaAereaViewModel>();
            }
            else
            {
                return Enumerable.Empty<CiaAereaViewModel>();
            }
            return _ciaaereasVM;
        }
    }

    public async Task<CiaAereaViewModel> GetCiaAereaPorId(int id)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var ciaaerea = await JsonSerializer
                    .DeserializeAsync<CiaAereaViewModel>(apiResponse, _options);
                _ciaaereaVM = ciaaerea ?? new CiaAereaViewModel();
            }
            else
            {
                return new CiaAereaViewModel();
            }
            return _ciaaereaVM;
        }
    }

    public async Task<CiaAereaViewModel> CriaCiaAerea(CiaAereaViewModel ciaaereaVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        var content = new StringContent(
            JsonSerializer.Serialize(ciaaereaVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var createdCiaAerea = await JsonSerializer
                    .DeserializeAsync<CiaAereaViewModel>(apiResponse, _options);
                _ciaaereaVM = createdCiaAerea ?? new CiaAereaViewModel();
            }
            else
            {
                return new CiaAereaViewModel();
            }
            return _ciaaereaVM;
        }
    }

    public async Task<bool> AtualizaCiaAerea(int id, CiaAereaViewModel ciaaereaVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, ciaaereaVM))
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
    public async Task<bool> DeletaCiaAerea(int id)
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

    Task<CiaAereaViewModel> ICiaAereaService.AtualizaCiaAerea(int id, CiaAereaViewModel vooVM)
    {
        throw new NotImplementedException();
    }

    Task<CiaAereaViewModel> ICiaAereaService.DeletaCiaAerea(int id)
    {
        throw new NotImplementedException();
    }
}

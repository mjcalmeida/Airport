using Airport.Models;
using System.Text;
using System.Text.Json;

namespace Airport.Services;
public class VooService : IVooService
{
    private const string apiEndpoint = "/api/v1/Voos";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private VooViewModel _vooVM = new VooViewModel();
    private IEnumerable<VooViewModel> _voosVM = new List<VooViewModel>();

    public VooService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<VooViewModel>> GetVoos()
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var voos = await JsonSerializer
                    .DeserializeAsync<IEnumerable<VooViewModel>>(apiResponse, _options);
                _voosVM = voos ?? new List<VooViewModel>();
            }
            else
            {
                return Enumerable.Empty<VooViewModel>();
            }
            return _voosVM;
        }
    }

    public async Task<VooViewModel> GetVooPorId(int id)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var voo = await JsonSerializer
                    .DeserializeAsync<VooViewModel>(apiResponse, _options);
                _vooVM = voo ?? new VooViewModel();
            }
            else
            {
                return new VooViewModel();
            }
            return _vooVM;
        }
    }

    public async Task<VooViewModel> CriaVoo(VooViewModel vooVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        var content = new StringContent(
            JsonSerializer.Serialize(vooVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var createdVoo = await JsonSerializer
                    .DeserializeAsync<VooViewModel>(apiResponse, _options);
                _vooVM = createdVoo ?? new VooViewModel();
            }
            else
            {
                return new VooViewModel();
            }
            return _vooVM;
        }
    }

    public async Task<bool> AtualizaVoo(int id, VooViewModel vooVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, vooVM))
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
    public async Task<bool> DeletaVoo(int id)
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

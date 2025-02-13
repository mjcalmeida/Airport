using Airport.Models;
using System.Text;
using System.Text.Json;

namespace Airport.Services;
public class AgendaService : IAgendaService
{
    private const string apiEndpoint = "/api/v1/Agenda";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private AgendaViewModel _agendaVM = new AgendaViewModel();
    private IEnumerable<AgendaViewModel> _agendasVM = new List<AgendaViewModel>();

    public AgendaService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<AgendaViewModel>> GetAgenda()
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var agendas = await JsonSerializer
                    .DeserializeAsync<IEnumerable<AgendaViewModel>>(apiResponse, _options);
                _agendasVM = agendas ?? new List<AgendaViewModel>();
            }
            else
            {
                return Enumerable.Empty<AgendaViewModel>();
            }
            return _agendasVM;
        }
    }

    public async Task<bool> AtualizaAgenda(int id, AgendaViewModel agendaVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, agendaVM))
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

    public async Task<AgendaViewModel> CriaAgenda(AgendaViewModel agendaVM)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        var content = new StringContent(
            JsonSerializer.Serialize(agendaVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var agenda = await JsonSerializer
                    .DeserializeAsync<AgendaViewModel>(apiResponse, _options);
                _agendaVM = agenda ?? new AgendaViewModel { Horario = string.Empty };
            }
            else
            {
                return new AgendaViewModel { Horario = string.Empty };
            }
            return _agendaVM;
        }
    }

    public async Task<bool> DeletaAgenda(int id)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }

    public async Task<AgendaViewModel> GetAgendaPorId(int id)
    {
        var client = _clientFactory.CreateClient("AeroportoAPI");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var agenda = await JsonSerializer
                    .DeserializeAsync<AgendaViewModel>(apiResponse, _options);
                _agendaVM = agenda ?? new AgendaViewModel { Horario = string.Empty };
            }
            else
            {
                return new AgendaViewModel { Horario = string.Empty };
            }
            return _agendaVM;
        }
    }
}

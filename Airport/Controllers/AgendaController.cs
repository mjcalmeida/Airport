using Airport.Models;
using Airport.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers;
public class AgendaController : Controller
{
    private readonly IAgendaService _agendaService;

    public AgendaController(IAgendaService agendaService)
    {
        _agendaService = agendaService;
    }

    public async Task<ActionResult<IEnumerable<AgendaViewModel>>> Index()
    {
        var result = await _agendaService.GetAgenda();
        if (result == null)
        {
            return View("Error");
        }

        return View(result);
    }
}
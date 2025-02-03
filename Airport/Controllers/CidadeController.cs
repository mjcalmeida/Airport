using Airport.Models;
using Airport.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers;
public class CidadeController : Controller
{
    private readonly ICidadeService _cidadeService;

    public CidadeController(ICidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }

    public async Task<ActionResult<IEnumerable<CidadeViewModel>>> Index()
    {
        var result = await _cidadeService.GetCidades();
        if(result == null)
        {
            return View("Error");
        }

        return View(result);
    }
}
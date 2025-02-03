using Airport.Models;
using Airport.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers;
public class CiaAereaController : Controller
{
    private readonly ICiaAereaService _ciaaereaService;

    public CiaAereaController(ICiaAereaService ciaaereaService)
    {
        _ciaaereaService = ciaaereaService;
    }

    public async Task<ActionResult<IEnumerable<CiaAereaViewModel>>> Index()
    {
        var result = await _ciaaereaService.GetCiaAerea();
        if(result == null)
        {
            return View("Error");
        }

        return View(result);
    }
}
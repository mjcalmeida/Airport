using Airport.Models;
using Airport.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers;
public class VooController : Controller
{
    private readonly IVooService _vooService;

    public VooController(IVooService vooService)
    {
        _vooService = vooService;
    }

    public async Task<ActionResult<IEnumerable<VooViewModel>>> Index()
    {
        var result = await _vooService.GetVoos();
        if(result == null)
        {
            return View("Error");
        }

        return View(result);
    }
}
using AuxiliarContabil.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuxiliarContabil.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeriadosController(IFeriadosService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("{dataInicial}/{dataFinal}")]
    public async Task<IActionResult> GetByDateRange(DateTime dataInicial, DateTime dataFinal) => Ok(await service.GetByDateRange(dataInicial, dataFinal));
}
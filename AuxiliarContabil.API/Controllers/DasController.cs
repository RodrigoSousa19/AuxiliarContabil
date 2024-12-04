using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuxiliarContabil.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DasController(IDasService dasService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await dasService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var das = await dasService.GetByIdAsync(id);
        return das == null ? NotFound() : Ok(das);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DasDto dasDto)
    {
        await dasService.AddAsync(dasDto);
        return CreatedAtAction(nameof(GetById), new { id = dasDto.Id }, dasDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DasDto dasDto)
    {
        if (id != dasDto.Id) return BadRequest();
        await dasService.UpdateAsync(dasDto);
        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await dasService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("faixaatual/{salarioBrutoAnual}")]
    public async Task<IActionResult> GetFaixaAtual(decimal salarioBrutoAnual)
    {
        var faixaAtual = await dasService.GetFaixaDas(salarioBrutoAnual);
        return faixaAtual == null ? NotFound() : Ok(faixaAtual);
    }
}
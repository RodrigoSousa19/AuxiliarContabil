using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuxiliarContabil.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComposicaoSalarialController : ControllerBase
{
    private readonly IComposicaoSalarialService _composicaoSalarialService;

    public ComposicaoSalarialController(IComposicaoSalarialService composicaoSalarialService)
    {
        _composicaoSalarialService = composicaoSalarialService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _composicaoSalarialService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var composicao = await _composicaoSalarialService.GetByIdAsync(id);
        return composicao == null ? NotFound() : Ok(composicao);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComposicaoSalarioDto composicaoDto)
    {
        await _composicaoSalarialService.AddAsync(composicaoDto);
        return CreatedAtAction(nameof(GetById), new { id = composicaoDto.Id }, composicaoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ComposicaoSalarioDto composicaoDto)
    {
        if (id != composicaoDto.Id) return BadRequest();
        await _composicaoSalarialService.UpdateAsync(composicaoDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _composicaoSalarialService.DeleteAsync(id);
        return NoContent();
    }
}
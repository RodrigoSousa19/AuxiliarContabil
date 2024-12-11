using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuxiliarContabil.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExtratoBancarioController(IExtratoBancarioService extratoBancarioService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await extratoBancarioService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var extrato = await extratoBancarioService.GetByIdAsync(id);
        return extrato == null ? NotFound() : Ok(extrato);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExtratoBancarioPessoaJuridicaDTO extratoDto)
    {
        await extratoBancarioService.AddAsync(extratoDto);
        return CreatedAtAction(nameof(GetById), new { id = extratoDto.Id }, extratoDto);
    }
    
    [HttpPost("inserirlista")]
    public async Task<IActionResult> Create(IEnumerable<ExtratoBancarioPessoaJuridicaDTO> extratoDto)
    {
        await extratoBancarioService.AddAsync(extratoDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExtratoBancarioPessoaJuridicaDTO extratoDto)
    {
        if (id != extratoDto.Id) return BadRequest();
        await extratoBancarioService.UpdateAsync(extratoDto);
        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await extratoBancarioService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("agrupados")]
    public async Task<IActionResult> GetAllAgrupados() => Ok( await extratoBancarioService.GetAllByBankAndType());
}
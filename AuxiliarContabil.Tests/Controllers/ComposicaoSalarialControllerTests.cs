using AuxiliarContabil.API.Controllers;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuxiliarContabil.Tests.Controllers;

public class ComposicaoSalarialControllerTests
{
    private readonly Mock<IComposicaoSalarialService> _service;
    private readonly ComposicaoSalarialController _controller;
    
    public ComposicaoSalarialControllerTests()
    {
        _service = new Mock<IComposicaoSalarialService>();
        _controller = new ComposicaoSalarialController(_service.Object);
    }

    
    [Fact]
    public async Task GetById_ShouldReturnOk_WhenComposicaoSalarialExists()
    {
        var dto = new ComposicaoSalarioDto
        {
            Id = 1,
            InicioPeriodo = new DateTime(2024, 01, 01),
            FimPeriodo = new DateTime(2024, 01, 31),
            QuantidadeDiasUteis = 22,
            Das = 200.00m,
            Gps = 300.00m,
            ProLabore = 5000.00m,
            SalarioHora = 30.00m,
            SalarioDia = 240.00m,
            SalarioBruto = 5000.00m,
            MensalidadeContabilidade = 800.00m,
            ComposicaoAtual = true
        };
        
        _service.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);
        
        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseDto = Assert.IsType<ComposicaoSalarioDto>(okResult.Value);

        Assert.Equal(1, responseDto.Id);
        Assert.Equal(22, responseDto.QuantidadeDiasUteis);
        Assert.Equal(200.00m, responseDto.Das);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenComposicaoSalarialDoesNotExist()
    {
        _service.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ComposicaoSalarioDto)null);
        
        var result = await _controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WhenComposicoesExist()
    {
        var dtoList = new List<ComposicaoSalarioDto>
        {
            new ComposicaoSalarioDto
            {
                Id = 1, InicioPeriodo = DateTime.Now, FimPeriodo = DateTime.Now.AddMonths(1), QuantidadeDiasUteis = 22,
                Das = 200.00m, Gps = 300.00m, ProLabore = 5000.00m
            },
            new ComposicaoSalarioDto
            {
                Id = 2, InicioPeriodo = DateTime.Now.AddMonths(1), FimPeriodo = DateTime.Now.AddMonths(2),
                QuantidadeDiasUteis = 22, Das = 250.00m, Gps = 350.00m, ProLabore = 5500.00m
            }
        };

        _service.Setup(s => s.GetAllAsync()).ReturnsAsync(dtoList);
        
        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseList = Assert.IsType<List<ComposicaoSalarioDto>>(okResult.Value);

        Assert.Equal(2, responseList.Count);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction_WhenComposicaoIsCreated()
    {
        var dto = new ComposicaoSalarioDto
        {
            Id = 1,
            InicioPeriodo = new DateTime(2024, 01, 01),
            FimPeriodo = new DateTime(2024, 01, 31),
            QuantidadeDiasUteis = 22,
            Das = 200.00m,
            Gps = 300.00m,
            ProLabore = 5000.00m,
            SalarioHora = 30.00m,
            SalarioDia = 240.00m,
            SalarioBruto = 5000.00m,
            MensalidadeContabilidade = 800.00m,
            ComposicaoAtual = true
        };

        _service.Setup(s => s.AddAsync(dto)).Returns(Task.CompletedTask);
        
        var result = await _controller.Create(dto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        
        Assert.Equal("GetById", createdResult.ActionName);
        
        Assert.Equal(1, createdResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenComposicaoIsUpdated()
    {
        var dto = new ComposicaoSalarioDto
        {
            Id = 1,
            InicioPeriodo = new DateTime(2024, 01, 01),
            FimPeriodo = new DateTime(2024, 01, 31),
            QuantidadeDiasUteis = 22,
            Das = 200.00m,
            Gps = 300.00m,
            ProLabore = 5000.00m,
            SalarioHora = 30.00m,
            SalarioDia = 240.00m,
            SalarioBruto = 5000.00m,
            MensalidadeContabilidade = 800.00m,
            ComposicaoAtual = true
        };

        _service.Setup(s => s.UpdateAsync(dto)).Returns(Task.CompletedTask);
        
        var result = await _controller.Update(1, dto);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenComposicaoIsDeleted()
    {
        _service.Setup(s => s.DeleteAsync(1)).Returns(Task.CompletedTask);
        
        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }
}
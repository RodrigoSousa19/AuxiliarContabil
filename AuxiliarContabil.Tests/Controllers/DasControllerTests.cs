using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class DasControllerTests
{
    private readonly Mock<IDasService> _service;
    private readonly DasController _controller;

    public DasControllerTests()
    {
        _service = new Mock<IDasService>();
        _controller = new DasController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WhenDasExist()
    {
        var dasList = new List<DasDto>
        {
            new DasDto { Id = 1, Faixa = "Faixa 1", ReceitaBrutaAnual = 100000m, Aliquota = 5.5m },
            new DasDto { Id = 2, Faixa = "Faixa 2", ReceitaBrutaAnual = 200000m, Aliquota = 6.5m }
        };

        _service.Setup(s => s.GetAllAsync()).ReturnsAsync(dasList);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseList = Assert.IsType<List<DasDto>>(okResult.Value);

        Assert.Equal(2, responseList.Count);
        Assert.Equal("Faixa 1", responseList[0].Faixa);
        Assert.Equal(100000m, responseList[0].ReceitaBrutaAnual);
        Assert.Equal(5.5m, responseList[0].Aliquota);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnOk_WhenDasExists()
    {
        var dasDto = new DasDto
        {
            Id = 1,
            Faixa = "Faixa 1",
            ReceitaBrutaAnual = 100000m,
            Aliquota = 5.5m
        };

        _service.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dasDto);
        
        var result = await _controller.GetById(1);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseDto = Assert.IsType<DasDto>(okResult.Value);

        Assert.Equal(1, responseDto.Id);
        Assert.Equal("Faixa 1", responseDto.Faixa);
        Assert.Equal(100000m, responseDto.ReceitaBrutaAnual);
        Assert.Equal(5.5m, responseDto.Aliquota);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenDasDoesNotExist()
    {
        _service.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((DasDto)null);
        
        var result = await _controller.GetById(999); // ID não existente
        
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction_WhenDasIsCreated()
    {
        var dasDto = new DasDto
        {
            Id = 1,
            Faixa = "Faixa 1",
            ReceitaBrutaAnual = 100000m,
            Aliquota = 5.5m
        };

        _service.Setup(s => s.AddAsync(dasDto)).Returns(Task.CompletedTask);

        var result = await _controller.Create(dasDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetById", createdResult.ActionName);
        Assert.Equal(1, createdResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenDasIsUpdated()
    {
        var dasDto = new DasDto
        {
            Id = 1,
            Faixa = "Faixa 1",
            ReceitaBrutaAnual = 200000m,
            Aliquota = 6.5m
        };

        _service.Setup(s => s.UpdateAsync(dasDto)).Returns(Task.CompletedTask);

        var result = await _controller.Update(1, dasDto);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenDasIsDeleted()
    {
        _service.Setup(s => s.DeleteAsync(1)).Returns(Task.CompletedTask);
        
        var result = await _controller.Delete(1);
        
        Assert.IsType<NoContentResult>(result);
    }

}
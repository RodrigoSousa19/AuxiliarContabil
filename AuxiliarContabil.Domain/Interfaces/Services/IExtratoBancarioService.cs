﻿using System.Reflection;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Entities;
using AuxiliarContabil.Domain.Models;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IExtratoBancarioService
{
    Task<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>> GetAllAsync();
    Task<ExtratoBancarioPessoaJuridicaDTO?> GetByIdAsync(int id);
    Task AddAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto);
    Task AddAsync(IEnumerable<ExtratoBancarioPessoaJuridicaDTO> extratoDto);
    Task UpdateAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto);
    Task DeleteAsync(int id);
    Task<IEnumerable<ResumoExtrato>> GetAllByBankAndType();
    Task<List<ExtratoBancarioPessoaJuridica>> ProcessarArquivoOfx(Stream arquivoStream);
}
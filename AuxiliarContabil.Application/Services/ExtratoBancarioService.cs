using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Entities;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.Domain.Models;

namespace AuxiliarContabil.Application.Services;

public class ExtratoBancarioService : IExtratoBancarioService
{
    private readonly IRepository<ExtratoBancarioPessoaJuridica> _repository;
    private readonly IMapper _mapper;
    private const int seed = 50;

    public ExtratoBancarioService(IRepository<ExtratoBancarioPessoaJuridica> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>> GetAllAsync()
    {
        var extratos = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>>(extratos);
    }

    public async Task<ExtratoBancarioPessoaJuridicaDTO?> GetByIdAsync(int id)
    {
        var extrato = await _repository.GetByIdAsync(id);
        return _mapper.Map<ExtratoBancarioPessoaJuridicaDTO>(extrato);
    }

    public async Task AddAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto)
    {
        var extrato = _mapper.Map<ExtratoBancarioPessoaJuridica>(extratoDto);
        await _repository.AddAsync(extrato);
    }

    public async Task AddAsync(IEnumerable<ExtratoBancarioPessoaJuridicaDTO> extratos)
    {
        var loteExtratos = extratos.GroupBy(x => (x.NomeBanco, x.TipoTransacao)).ToList();

        for (int lote = 0; lote < loteExtratos.Count; lote++)
        {
            for (int itens = 0; itens < loteExtratos[lote].Count(); itens += seed)
            {
                var itensDto = loteExtratos[lote].Skip(itens).Take(seed).ToList();
                var itensToInsert = _mapper.Map<List<ExtratoBancarioPessoaJuridica>>(itensDto);
                await _repository.AddRangeAsync(itensToInsert);
            }
        }
    }

    public async Task UpdateAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto)
    {
        var extrato = _mapper.Map<ExtratoBancarioPessoaJuridica>(extratoDto);
        await _repository.UpdateAsync(extrato);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    
    public async Task<IEnumerable<ResumoExtrato>> GetAllByBankAndType()
    {
        var extratosDto = _mapper.Map<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>>(await _repository.GetAllAsync());

        IEnumerable<ResumoExtrato> resumo = extratosDto
            .GroupBy(x => x.NomeBanco)
            .Select(bancoGroup => new ResumoExtrato
            {
                Banco = bancoGroup.Key,
                Movimentacoes = bancoGroup
                    .GroupBy(x => x.TipoTransacao)
                    .Select(tipoGroup => new MovimentacaoExtrato()
                    {
                        Tipo = tipoGroup.Key.ToString(),
                        Transacoes = tipoGroup.Select(t => new Transacao
                        {
                            Valor = t.ValorTransacao,
                            Descricao = t.Descricao,
                            Date = t.DataTransacao
                        }).OrderByDescending(d => d.Date).ToList()
                    })
            });
        
        return resumo;
    }
    
    public async Task<List<ExtratoBancarioPessoaJuridica>> ProcessarArquivoOfx(Stream arquivoStream)
    {
        var transacoes = new List<ExtratoBancarioPessoaJuridica>();

        using var reader = new StreamReader(arquivoStream);
        string ofxContent = reader.ReadToEnd();
        
        if (string.IsNullOrWhiteSpace(ofxContent))
        {
            throw new ArgumentException("O conteúdo do arquivo OFX está vazio.");
        }

        string banco = ExtractTag(ofxContent, "BANKID") + " - " + ExtractTag(ofxContent, "ORG");

        var transacoesRaw = Regex.Split(ofxContent, @"<STMTTRN>");

        foreach (var transacaoRaw in transacoesRaw)
        {
            if (!transacaoRaw.Contains("<TRNAMT>")) continue;

            var dataTransacao = DateTime.ParseExact(ExtractTag(transacaoRaw, "DTPOSTED").Substring(0, 8), "yyyyMMdd", null);
            var tipoTransacao = ExtractTag(transacaoRaw, "TRNTYPE") == "DEBIT" ? "Débito" : "Crédito";

            var valorStr = ExtractTag(transacaoRaw, "TRNAMT");
            if (valorStr.StartsWith("-"))
            {
                valorStr = valorStr.Substring(1);
            }
            var valor = decimal.Parse(valorStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            var descricao = ExtractTag(transacaoRaw, "MEMO");

            var transacao = new ExtratoBancarioPessoaJuridica
            {
                NomeBanco = banco,
                DataTransacao = dataTransacao,
                TipoTransacao = tipoTransacao,
                ValorTransacao = valor,
                Descricao = descricao
            };

            transacoes.Add(transacao);
        }

        foreach (var transacao in transacoes)
        {
            _repository.AddAsync(transacao);
        }
        return transacoes;
    }
    
    private static string ExtractTag(string conteudo, string tag)
    {
        var match = Regex.Match(conteudo, $@"<{tag}>(.*?)<");
        return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
    }

}
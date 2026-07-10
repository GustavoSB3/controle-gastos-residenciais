using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class TotaisService(AppDbContext db)
{
    public async Task<TotaisDto> ObterAsync()
    {
        // O SQLite não consegue somar decimal direto nesta consulta.
        // Por isso buscamos os dados primeiro e fazemos as contas logo abaixo.
        var dados = await db.Pessoas.AsNoTracking()
            .OrderBy(p => p.Nome)
            .Select(p => new
            {
                p.Id,
                p.Nome,
                Transacoes = p.Transacoes.Select(t => new { t.Tipo, t.Valor }).ToList()
            })
            .ToListAsync();

        // Calcula as receitas e despesas separadamente para cada pessoa.
        var pessoas = dados.Select(p =>
        {
            var receitas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);
            var despesas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            return new TotalPessoaDto(p.Id, p.Nome, receitas, despesas, receitas - despesas);
        }).ToList();

        var receitas = pessoas.Sum(p => p.TotalReceitas);
        var despesas = pessoas.Sum(p => p.TotalDespesas);
        return new TotaisDto(pessoas, receitas, despesas, receitas - despesas);
    }
}

using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class TotaisService(AppDbContext db)
{
    public async Task<TotaisDto> ObterAsync()
    {
        // Os agrupamentos são calculados no banco e incluem pessoas sem transações.
        var pessoas = await db.Pessoas.AsNoTracking().OrderBy(p => p.Nome)
            .Select(p => new TotalPessoaDto(
                p.Id,
                p.Nome,
                p.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => (decimal?)t.Valor) ?? 0,
                p.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => (decimal?)t.Valor) ?? 0,
                (p.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => (decimal?)t.Valor) ?? 0) -
                (p.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => (decimal?)t.Valor) ?? 0)))
            .ToListAsync();

        var receitas = pessoas.Sum(p => p.TotalReceitas);
        var despesas = pessoas.Sum(p => p.TotalDespesas);
        return new TotaisDto(pessoas, receitas, despesas, receitas - despesas);
    }
}

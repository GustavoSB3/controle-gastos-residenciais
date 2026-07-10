using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class TransacaoService(AppDbContext db)
{
    public async Task<IReadOnlyCollection<TransacaoDto>> ListarAsync() =>
        await db.Transacoes.AsNoTracking().OrderByDescending(t => t.Id)
            .Select(t => new TransacaoDto(t.Id, t.Descricao, t.Valor, t.Tipo, t.PessoaId, t.Pessoa.Nome))
            .ToListAsync();

    public async Task<(TransacaoDto? Transacao, string? Erro)> CriarAsync(CriarTransacaoDto dto)
    {
        // Primeiro confere se o id recebido pertence a uma pessoa cadastrada.
        var pessoa = await db.Pessoas.FindAsync(dto.PessoaId);
        if (pessoa is null) return (null, "A pessoa informada não existe.");

        // Esta é a regra do sistema: menor de idade só pode cadastrar despesa.
        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
            return (null, "Pessoas menores de 18 anos só podem registrar despesas.");

        var transacao = new Transacao
        {
            Descricao = dto.Descricao.Trim(), Valor = dto.Valor, Tipo = dto.Tipo, PessoaId = dto.PessoaId
        };
        db.Transacoes.Add(transacao);
        await db.SaveChangesAsync();
        return (new TransacaoDto(transacao.Id, transacao.Descricao, transacao.Valor,
            transacao.Tipo, transacao.PessoaId, pessoa.Nome), null);
    }
}

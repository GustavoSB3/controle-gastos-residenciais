using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class PessoaService(AppDbContext db)
{
    public async Task<IReadOnlyCollection<PessoaDto>> ListarAsync() =>
        await db.Pessoas.AsNoTracking().OrderBy(p => p.Nome)
            .Select(p => new PessoaDto(p.Id, p.Nome, p.Idade)).ToListAsync();

    public async Task<PessoaDto> CriarAsync(CriarPessoaDto dto)
    {
        var pessoa = new Pessoa { Nome = dto.Nome.Trim(), Idade = dto.Idade };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();
        return new PessoaDto(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var pessoa = await db.Pessoas.FindAsync(id);
        // Retorna false para o controller responder com 404 quando não encontrar.
        if (pessoa is null) return false;
        db.Pessoas.Remove(pessoa);
        await db.SaveChangesAsync();
        return true;
    }
}

namespace ControleGastos.Api.DTOs;

public record TotalPessoaDto(int PessoaId, string Nome, decimal TotalReceitas, decimal TotalDespesas, decimal Saldo);

public record TotaisDto(
    IReadOnlyCollection<TotalPessoaDto> Pessoas,
    decimal TotalGeralReceitas,
    decimal TotalGeralDespesas,
    decimal SaldoLiquidoGeral);

using System.ComponentModel.DataAnnotations;
using ControleGastos.Api.Models;

namespace ControleGastos.Api.DTOs;

public record CriarTransacaoDto(
    [Required, StringLength(200, MinimumLength = 2)] string Descricao,
    [Range(typeof(decimal), "0.01", "9999999999999999")] decimal Valor,
    TipoTransacao Tipo,
    [Range(1, int.MaxValue)] int PessoaId);

public record TransacaoDto(
    int Id, string Descricao, decimal Valor, TipoTransacao Tipo, int PessoaId, string PessoaNome);

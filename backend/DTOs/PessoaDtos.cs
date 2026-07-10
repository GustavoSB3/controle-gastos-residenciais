using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

public record CriarPessoaDto(
    [Required, StringLength(120, MinimumLength = 2)] string Nome,
    [Range(0, 130)] int Idade);

public record PessoaDto(int Id, string Nome, int Idade);

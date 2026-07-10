using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController(PessoaService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PessoaDto>>> Listar() => Ok(await service.ListarAsync());

    [HttpPost]
    public async Task<ActionResult<PessoaDto>> Criar(CriarPessoaDto dto)
    {
        var pessoa = await service.CriarAsync(dto);
        return Created($"api/pessoas/{pessoa.Id}", pessoa);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id) =>
        await service.ExcluirAsync(id) ? NoContent() : NotFound(new { mensagem = "Pessoa não encontrada." });
}

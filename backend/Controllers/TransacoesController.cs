using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController(TransacaoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TransacaoDto>>> Listar() => Ok(await service.ListarAsync());

    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> Criar(CriarTransacaoDto dto)
    {
        var (transacao, erro) = await service.CriarAsync(dto);
        if (erro is not null) return BadRequest(new { mensagem = erro });
        return Created($"api/transacoes/{transacao!.Id}", transacao);
    }
}

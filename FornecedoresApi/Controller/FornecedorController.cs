using FornecedoresApi.Domain.Entidade;
using FornecedoresApi.Domain.IApplication;
using FornecedoresApi.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace FornecedoresApi.Controller;


[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorService _fornecedorService;

    public FornecedorController(IFornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    /// <summary>
    /// Endpoint usado para retornar lista de fornecedores e pode ser passado o Id para retornar um específico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Fornecedor>> BuscaFornecedores(int? id)
    {
        var fornecedores = await _fornecedorService.GetListaFornecedores(id);
        return fornecedores.Any() ? Ok(fornecedores) : NoContent();
    }

    /// <summary>
    /// Endpoint usado para Incluir novo Fornecdor
    /// </summary>
    /// <param name="fornecedor"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> IncluiFornecedor(FornecedorModel fornecedor)
    {
        await _fornecedorService.IncluiFornecedor(fornecedor);
        return Ok();
    }

    /// <summary>
    /// Endpoit usado para atualizar dados do Fornecedor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fornecedorModel"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> AtualizaFornecedor(int id, FornecedorModel fornecedorModel)
    {
        await _fornecedorService.AtualizaFornecedor(id, fornecedorModel);
        return Ok();
    }

    /// <summary>
    /// Endponit usa para Deletar Fornecedor
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletaFornecedor(int id)
    {
        await _fornecedorService.DeletaFornecedor(id);
        return Ok();
    }
}

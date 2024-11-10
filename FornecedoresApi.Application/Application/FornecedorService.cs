using FornecedoresApi.Domain.Entidade;
using FornecedoresApi.Domain.IApplication;
using FornecedoresApi.Domain.IRepository;
using FornecedoresApi.Domain.Model;

namespace FornecedoresApi.Application.Application;

public class FornecedorService : IFornecedorService
{
    private readonly IFornecedorRepository _fornecedorRepository;
    public FornecedorService(IFornecedorRepository fornecedorRepository)
    {
        _fornecedorRepository = fornecedorRepository;
    }

    public async Task<List<Fornecedor>> GetListaFornecedores(int? id)
    {
        if (id is null)
            return await _fornecedorRepository.GetAllAsync();
        var fornecedor = await _fornecedorRepository.GetByIdAsync(id.Value);
        return fornecedor != null ? new List<Fornecedor> { fornecedor } : new();
    }

    public async Task IncluiFornecedor(FornecedorModel fornecedorModel)
    {
        var novoFornecedor = new Fornecedor()
        {
            Nome = fornecedorModel.Nome,
            Email = fornecedorModel.Email,
            Endereco = fornecedorModel.Endereco,
            Telefone = fornecedorModel.Telefone
        };

        var result = new FornecedorValidator().Validate(novoFornecedor);

        if (result.Errors.Count > 0)
            throw new ArgumentException($"{result}");

        await _fornecedorRepository.AddAsync(novoFornecedor);
    }

    public async Task AtualizaFornecedor(int id, FornecedorModel fornecedorModel)
    {
        var fornecedor = await _fornecedorRepository.GetByIdAsync(id) ?? 
            throw new ArgumentNullException("ID não existe na base");

        fornecedor.AtualizaFornecedor(fornecedorModel);
        await _fornecedorRepository.UpdateAsync(fornecedor);
    }

    public async Task DeletaFornecedor(int id)
    {
        var fornecedor = await _fornecedorRepository.GetByIdAsync(id) ?? throw new ArgumentNullException("ID não existe na base");
        await _fornecedorRepository.DeleteAsync(fornecedor.Id);
    }
}

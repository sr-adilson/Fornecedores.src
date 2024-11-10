using FornecedoresApi.Domain.Entidade;
using FornecedoresApi.Domain.Model;

namespace FornecedoresApi.Domain.IApplication
{
    public interface IFornecedorService
    {
        Task<List<Fornecedor>>GetListaFornecedores(int? id);
        Task IncluiFornecedor(FornecedorModel fornecedor);
        Task AtualizaFornecedor(int id, FornecedorModel fornecedorModel);
        Task DeletaFornecedor(int id);
    }
}

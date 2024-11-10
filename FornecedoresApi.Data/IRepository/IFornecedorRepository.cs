using FornecedoresApi.Domain.Entidade;
using System.Security.Cryptography;

namespace FornecedoresApi.Domain.IRepository;
public interface IFornecedorRepository 
{
    Task<List<Fornecedor>> GetAllAsync();
    Task<Fornecedor> GetByIdAsync(int id);
    Task AddAsync(Fornecedor fornecedor);
    Task UpdateAsync(Fornecedor fornecedor);
    Task DeleteAsync(int id);
}

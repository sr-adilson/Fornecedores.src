using FornecedoresApi.Domain.Entidade;
using FornecedoresApi.Domain.IRepository;
using FornecedoresApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Persistence.Repositories;

public class FornecedorRepository : BaseRepository<Fornecedor, int>, IFornecedorRepository
{
    private readonly FornecedorDbContext _context;

    public FornecedorRepository(FornecedorDbContext context) : base(context)
    {
    }

}

using FornecedoresApi.Domain.Entidade;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Persistence.Context
{
    public class FornecedorDbContext : DbContext
    {
        public FornecedorDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Nome).IsRequired().HasMaxLength(255);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(255);
                entity.Property(x => x.Telefone).IsRequired().HasMaxLength(20);
                entity.Property(x => x.Endereco).IsRequired().HasMaxLength(500);
            });
        }
    }
}

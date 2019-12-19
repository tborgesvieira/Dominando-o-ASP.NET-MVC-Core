using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Nome)                
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Documento)                
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(c => c.Ativo)                
                .IsRequired();

            builder.Property(c => c.TipoFornecedor)                
                .IsRequired();

            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Fornecedor);

            builder.HasMany(p => p.Produtos)
                .WithOne(f => f.Fornecedor)
                .HasForeignKey(c => c.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}

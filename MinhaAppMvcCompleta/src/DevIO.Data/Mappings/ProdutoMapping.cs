using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)                
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Descricao)                
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(c => c.Imagem)                
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Produtos");
        }
    }
}

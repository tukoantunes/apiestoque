using ApiEstoque.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoque.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Produto
    /// </summary>
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO");

            builder.HasKey(p => p.IdProduto);

            builder.Property(p => p.IdProduto)
                .HasColumnName("IDPRODUTO");

            builder.Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasColumnName("PRECO")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Quantidade)
                .HasColumnName("QUANTIDADE")
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .HasColumnName("DATACRIACAO")
                .IsRequired();

            builder.Property(p => p.IdEstoque)
                .HasColumnName("IDESTOQUE")
                .IsRequired();

            builder.Property(p => p.IdUsuario)
                .HasColumnName("IDUSUARIO")
                .IsRequired();

            #region Mapeamento das chaves estrangeiras

            builder.HasOne(p => p.Estoque) //Produto PERTENCE a 1 Estoque
                .WithMany(e => e.Produtos) //Estoque TEM MUITOS Produtos
                .HasForeignKey(p => p.IdEstoque); //FOREIGN KEY

            builder.HasOne(p => p.Usuario) //Produto POSSUI 1 Usuário
                .WithMany(u => u.Produtos) //Usuario TEM MUITOS Produtos
                .HasForeignKey(p => p.IdUsuario) //FOREIGN KEY
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}




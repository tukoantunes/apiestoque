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
    /// Classe de mapeamento para a entidade Estoque
    /// </summary>
    public class EstoqueMap : IEntityTypeConfiguration<Estoque>
    {
        //método para mapeamento da entidade
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            //nome da tabela do banco de dados
            builder.ToTable("ESTOQUE");

            //mapear o campo chave primária
            builder.HasKey(e => e.IdEstoque);

            //mapear cada campo da tabela
            builder.Property(e => e.IdEstoque)
                .HasColumnName("IDESTOQUE");

            builder.Property(e => e.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.DataCriacao)
                .HasColumnName("DATACRIACAO")
                .IsRequired();

            builder.Property(e => e.IdUsuario)
                .HasColumnName("IDUSUARIO")
                .IsRequired();

            #region Mapeamento das chaves estrangeiras

            builder.HasOne(e => e.Usuario) //Estoque PERTENCE A 1 Usuário
                .WithMany(u => u.Estoques) //Usuario POSSUI MUITOS Estoques
                .HasForeignKey(e => e.IdUsuario) //campo FOREIGN KEY
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}




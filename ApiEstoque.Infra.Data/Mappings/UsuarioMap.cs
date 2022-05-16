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
    /// Classe de mapeamento para a entidade Usuario
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela
            builder.ToTable("USUARIO");

            //chave primária
            builder.HasKey(u => u.IdUsuario);

            //mapeamento dos campos da tabela
            builder.Property(u => u.IdUsuario)
                .HasColumnName("IDUSUARIO");

            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(u => u.DataCriacao)
                .HasColumnName("DATACRIACAO")
                .IsRequired();
        }
    }
}




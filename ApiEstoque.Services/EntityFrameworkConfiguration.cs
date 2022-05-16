using ApiEstoque.Infra.Data.Contexts;
using ApiEstoque.Infra.Data.Interfaces;
using ApiEstoque.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Services
{
    /// <summary>
    /// Classe para configuração do EntityFramework
    /// </summary>
    public class EntityFrameworkConfiguration
    {
        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            //capturar a connectionstring do banco de dados
            var connectionString = builder.Configuration.GetConnectionString("ApiEstoque");

            //injeção de dependencia para a classe SqlServerContext no EntityFramework
            builder.Services.AddDbContext<SqlServerContext>
                (map => map.UseSqlServer(connectionString));

            //mapear cada classe do repositorio
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<IEstoqueRepository, EstoqueRepository>();
            builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
        }
    }
}




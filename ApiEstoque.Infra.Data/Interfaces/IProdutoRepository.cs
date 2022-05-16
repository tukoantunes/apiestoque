using ApiEstoque.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoque.Infra.Data.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        List<Produto> GetAllByUsuario(Guid idUsuario);
    }
}




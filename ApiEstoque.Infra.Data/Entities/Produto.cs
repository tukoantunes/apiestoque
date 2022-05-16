using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoque.Infra.Data.Entities
{
    /// <summary>
    /// Classe de entidade
    /// </summary>
    public class Produto
    {
        #region Propriedades

        public Guid IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdEstoque { get; set; }

        #endregion

        #region Relacionamentos

        public Estoque Estoque { get; set; }
        public Usuario Usuario { get; set; }

        #endregion
    }
}




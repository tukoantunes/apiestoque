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
    public class Estoque
    {
        #region Propriedades

        public Guid IdEstoque { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid IdUsuario { get; set; }

        #endregion

        #region Relacionamentos

        public List<Produto> Produtos { get; set; }
        public Usuario Usuario { get; set; }

        #endregion
    }
}



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
    public class Usuario
    {
        #region Propriedades

        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }

        #endregion

        #region Relacionamentos

        public List<Estoque> Estoques { get; set; }
        public List<Produto> Produtos { get; set; }

        #endregion
    }
}




using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de cadastro de produto
    /// </summary>
    public class ProdutoPostRequest
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public Guid IdEstoque { get; set; }
    }
}





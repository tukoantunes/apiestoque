using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de cadastro de estoque
    /// </summary>
    public class EstoquePostRequest
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Descricao { get; set; }
    }
}




using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de atualização de estoque
    /// </summary>
    public class EstoquePutRequest
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public Guid IdEstoque { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Descricao { get; set; }
    }
}



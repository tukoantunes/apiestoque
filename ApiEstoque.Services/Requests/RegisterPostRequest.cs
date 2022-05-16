using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de cadastro de conta de usuário
    /// </summary>
    public class RegisterPostRequest
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "Mínimo de {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Máximo de {1} caracteres.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Senha { get; set; }
    }
}




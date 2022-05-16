using ApiEstoque.Infra.Data.Entities;
using ApiEstoque.Infra.Data.Interfaces;
using ApiEstoque.Infra.Data.Utils;
using ApiEstoque.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoque.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        //construtor para injeção de dependência
        public RegisterController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Post(RegisterPostRequest request)
        {
            try
            {
                //verificar se o email informado já está cadastrado no banco de dados
                if (_usuarioRepository.Get(request.Email) != null)
                    return StatusCode(422, new { message = "O email informado já está cadastrado, por favor verifique." });

                //cadastrando o usuário
                var usuario = new Usuario()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = Criptografia.Get(request.Senha, Hash.SHA1),
                    DataCriacao = DateTime.UtcNow
                };

                //gravando o usuário no banco de dados
                _usuarioRepository.Create(usuario);

                return StatusCode(201, new { message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}




using ApiEstoque.Infra.Data.Interfaces;
using ApiEstoque.Infra.Data.Utils;
using ApiEstoque.Services.Authentication;
using ApiEstoque.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoque.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenCreator _tokenCreator;

        public LoginController(IUsuarioRepository usuarioRepository, TokenCreator tokenCreator)
        {
            _usuarioRepository = usuarioRepository;
            _tokenCreator = tokenCreator;
        }

        [HttpPost]
        public IActionResult Post(LoginPostRequest request)
        {
            try
            {
                //buscando o usuário no banco de dados através do email e senha
                var usuario = _usuarioRepository.Get(request.Email, Criptografia.Get(request.Senha, Hash.SHA1));

                //verificar se o usuário foi encontrado
                if (usuario != null)
                {
                    //retornar resposta de sucesso com o token
                    return StatusCode(200, new
                    {
                        message = "Usuário autenticado com sucesso.",
                        nome = usuario.Nome,
                        email = usuario.Email,
                        accessToken = _tokenCreator.GenerateToken(usuario.Email)
                    });
                }
                else
                {
                    //HTTP STATUS 401 - UNAUTHORIZED
                    return StatusCode(401, new { message = "Acesso não autorizado, email e senha inválidos." });
                }
            }
            catch (Exception e)
            {
                //HTTP STATUS 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}




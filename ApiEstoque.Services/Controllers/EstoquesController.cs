using ApiEstoque.Infra.Data.Entities;
using ApiEstoque.Infra.Data.Interfaces;
using ApiEstoque.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoque.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquesController : ControllerBase
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EstoquesController(IEstoqueRepository estoqueRepository, IUsuarioRepository usuarioRepository)
        {
            _estoqueRepository = estoqueRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Post(EstoquePostRequest request)
        {
            try
            {
                var usuario = ObterUsuario();

                #region Realizar o cadastro do estoque

                var estoque = new Estoque()
                {
                    IdEstoque = Guid.NewGuid(),
                    Nome = request.Nome,
                    Descricao = request.Descricao,
                    DataCriacao = DateTime.UtcNow,
                    IdUsuario = usuario.IdUsuario
                };

                _estoqueRepository.Create(estoque);

                return StatusCode(201, new { message = "Estoque cadastrado com sucesso.", estoque });

                #endregion
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(EstoquePutRequest request)
        {
            try
            {
                var usuario = ObterUsuario();

                #region Buscando o Estoque no banco de dados através do ID

                var estoque = _estoqueRepository.GetById(request.IdEstoque);
                if (estoque == null || estoque.IdUsuario != usuario.IdUsuario)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { message = "Estoque não encontrado ou inválido para edição." });

                #endregion

                #region Atualizando o Estoque

                estoque.Nome = request.Nome;
                estoque.Descricao = request.Descricao;

                _estoqueRepository.Update(estoque);

                return StatusCode(200, new { message = "Estoque atualizado com sucesso", estoque });

                #endregion
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpDelete("{idEstoque}")]
        public IActionResult Delete(Guid idEstoque)
        {
            try
            {
                var usuario = ObterUsuario();

                #region Buscando o Estoque no banco de dados através do ID

                var estoque = _estoqueRepository.GetById(idEstoque);
                if (estoque == null || estoque.IdUsuario != usuario.IdUsuario)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { message = "Estoque não encontrado ou inválido para edição." });

                #endregion

                #region Excluindo o Estoque

                _estoqueRepository.Delete(estoque);

                return StatusCode(200, new { message = "Estoque excluído com sucesso", estoque });

                #endregion
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usuario = ObterUsuario();
                var estoques = _estoqueRepository.GetAllByUsuario(usuario.IdUsuario);

                //HTTP 200 (OK)
                return StatusCode(200, estoques);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet("{idEstoque}")]
        public IActionResult GetById(Guid idEstoque)
        {
            try
            {
                var usuario = ObterUsuario();
                var estoque = _estoqueRepository.GetById(idEstoque);

                if (estoque != null && estoque.IdUsuario == usuario.IdUsuario)
                    //HTTP 200 (OK)
                    return StatusCode(200, estoque);
                else
                    //HTTP 204 (NO CONTENT)
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        private Usuario ObterUsuario()
        {
            #region Capturar o usuário autenticado na API

            //O TOKEN gerado pela API contem o email ('Name') do usuário autenticado.
            return _usuarioRepository.Get(User.Identity.Name);

            #endregion
        }
    }
}




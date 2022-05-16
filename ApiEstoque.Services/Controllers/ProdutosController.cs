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
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProdutosController(IProdutoRepository produtoRepository, IEstoqueRepository estoqueRepository, IUsuarioRepository usuarioRepository)
        {
            _produtoRepository = produtoRepository;
            _estoqueRepository = estoqueRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Post(ProdutoPostRequest request)
        {
            try
            {
                var usuario = ObterUsuario();
                var estoque = _estoqueRepository.GetById(request.IdEstoque);

                if (estoque == null || estoque.IdUsuario != usuario.IdUsuario)
                    return StatusCode(422, new { message = "Estoque não encontrado ou inválido." });

                var produto = new Produto()
                {
                    IdProduto = Guid.NewGuid(),
                    Nome = request.Nome,
                    Preco = request.Preco,
                    Quantidade = request.Quantidade,
                    DataCriacao = DateTime.UtcNow,
                    IdEstoque = estoque.IdEstoque,
                    IdUsuario = usuario.IdUsuario
                };

                _produtoRepository.Create(produto);
                produto.Estoque = estoque;

                return StatusCode(201, new { message = "Produto cadastrado com sucesso", produto });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(ProdutoPutRequest request)
        {
            try
            {
                var usuario = ObterUsuario();
                var produto = _produtoRepository.GetById(request.IdProduto);
                var estoque = _estoqueRepository.GetById(request.IdEstoque);

                if (produto == null || produto.IdUsuario != usuario.IdUsuario)
                    return StatusCode(422, new { message = "Produto não encontrado ou inválido." });

                if (estoque == null || estoque.IdUsuario != usuario.IdUsuario)
                    return StatusCode(422, new { message = "Estoque não encontrado ou inválido." });

                produto.Nome = request.Nome;
                produto.Preco = request.Preco;
                produto.Quantidade = request.Quantidade;
                produto.IdEstoque = request.IdEstoque;

                _produtoRepository.Update(produto);

                return StatusCode(200, new { message = "Produto atualizado com sucesso", produto });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpDelete("{idProduto}")]
        public IActionResult Delete(Guid idProduto)
        {
            try
            {
                var usuario = ObterUsuario();
                var produto = _produtoRepository.GetById(idProduto);

                if (produto == null || produto.IdUsuario != usuario.IdUsuario)
                    return StatusCode(422, new { message = "Produto não encontrado ou inválido." });

                _produtoRepository.Delete(produto);

                return StatusCode(200, new { message = "Produto excluído com sucesso", produto });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usuario = ObterUsuario();
                var produtos = _produtoRepository.GetAllByUsuario(usuario.IdUsuario);

                //HTTP 200 (OK)
                return StatusCode(200, produtos);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet("{idProduto}")]
        public IActionResult GetById(Guid idProduto)
        {
            try
            {
                var usuario = ObterUsuario();
                var produto = _produtoRepository.GetById(idProduto);

                if (produto != null && produto.IdUsuario == usuario.IdUsuario)
                    //HTTP 200 (OK)
                    return StatusCode(200, produto);
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




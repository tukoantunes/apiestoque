using ApiEstoque.Infra.Data.Entities;
using ApiEstoque.Services.Requests;
using ApiEstoque.Tests.Config;
using Bogus;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiEstoque.Tests
{
    public class ProdutosTest
    {
        private readonly string _endpointEstoque;
        private readonly string _endpointProduto;
        private readonly string _accessToken;

        public ProdutosTest()
        {
            _endpointEstoque = ApiConfig.GetEndpoint() + "/estoques";
            _endpointProduto = ApiConfig.GetEndpoint() + "/produtos";

            var authenticationConfig = new AuthenticationConfig();
            _accessToken = authenticationConfig.ObterTokenAcesso().Result;
        }

        [Fact]
        public async Task<ProdutoResult> Test_Post_Returns_Ok()
        {
            var httpClient = new HttpClient();

            #region Realizando o cadastro de um estoque

            var faker = new Faker("pt_BR");

            var requestEstoque = new EstoquePostRequest()
            {
                Nome = faker.Company.CompanyName(),
                Descricao = faker.Company.CompanyName()
            };

            var contentEstoque = new StringContent
                (JsonConvert.SerializeObject(requestEstoque), Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var responseEstoque = await httpClient.PostAsync(_endpointEstoque, contentEstoque);

            var resultEstoque = JsonConvert.DeserializeObject<EstoqueResult>
               (responseEstoque.Content.ReadAsStringAsync().Result);

            #endregion

            #region Realizando o cadastro de um produto

            var requestProduto = new ProdutoPostRequest()
            {
                Nome = faker.Commerce.ProductName(),
                Preco = decimal.Parse(faker.Commerce.Price()),
                Quantidade = 10,
                IdEstoque = resultEstoque.estoque.IdEstoque
            };

            var contentProduto = new StringContent
                (JsonConvert.SerializeObject(requestProduto), Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var responseProduto = await httpClient.PostAsync(_endpointProduto, contentProduto);

            responseProduto
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            #endregion

            //retornando os dados do produto cadastrado
            var resultProduto = JsonConvert.DeserializeObject<ProdutoResult>
               (responseProduto.Content.ReadAsStringAsync().Result);

            return resultProduto;
        }

        [Fact]
        public async Task Test_Put_Returns_Ok()
        {
            var result = await Test_Post_Returns_Ok();

            var faker = new Faker("pt_BR");

            //criando os dados para editar o produto
            var request = new ProdutoPutRequest
            {
                IdProduto = result.produto.IdProduto,
                Nome = faker.Commerce.ProductName(),
                Preco = decimal.Parse(faker.Commerce.Price()),
                Quantidade = 12,
                IdEstoque = result.produto.IdEstoque
            };

            var httpClient = new HttpClient();

            var content = new StringContent
               (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.PutAsync(_endpointProduto, content);

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_Delete_Returns_Ok()
        {
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.DeleteAsync(_endpointProduto + "/" + result.produto.IdProduto);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_GetAll_Returns_Ok()
        {
            await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.GetAsync(_endpointProduto);

            var result = JsonConvert.DeserializeObject<List<Produto>>
               (response.Content.ReadAsStringAsync().Result);

            result.
                Should()
                .NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Test_GetById_Returns_Ok()
        {
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.GetAsync(_endpointProduto + "/" + result.produto.IdProduto);

            var resposta = JsonConvert.DeserializeObject<Produto>
               (response.Content.ReadAsStringAsync().Result);

            resposta
                .Should()
                .NotBeNull();
        }
    }

    /// <summary>
    /// Classe para capturar o retorno do resultado dos testes POST, PUT ou DELETE
    /// </summary>
    public class ProdutoResult
    {
        public string message { get; set; }
        public Produto produto { get; set; }
    }
}




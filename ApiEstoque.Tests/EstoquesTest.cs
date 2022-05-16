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
    public class EstoquesTest
    {
        private readonly string _endpoint;
        private readonly string _accessToken;

        public EstoquesTest()
        {
            _endpoint = ApiConfig.GetEndpoint() + "/estoques";

            var authenticationConfig = new AuthenticationConfig();
            _accessToken = authenticationConfig.ObterTokenAcesso().Result;
        }

        [Fact]
        public async Task<EstoqueResult> Test_Post_Returns_Ok()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.PostAsync(_endpoint, CreateEstoqueData());

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<EstoqueResult>
               (response.Content.ReadAsStringAsync().Result);

            return result;
        }

        [Fact]
        public async Task Test_Put_Returns_Ok()
        {
            //Realizando o cadastro de um estoque
            var result = await Test_Post_Returns_Ok();

            var faker = new Faker("pt_BR");

            //criando os dados para editar o estoque
            var request = new EstoquePutRequest
            {
                IdEstoque = result.estoque.IdEstoque,
                Nome = faker.Company.CompanyName(),
                Descricao = faker.Company.CompanyName()
            };

            var httpClient = new HttpClient();

            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.PutAsync(_endpoint, content);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_Delete_Returns_Ok()
        {
            //Realizando o cadastro de um estoque
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.DeleteAsync(_endpoint + "/" + result.estoque.IdEstoque);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_GetAll_Returns_Ok()
        {
            //Realizando o cadastro de um estoque
            await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.GetAsync(_endpoint);

            var result = JsonConvert.DeserializeObject<List<Estoque>>
               (response.Content.ReadAsStringAsync().Result);

            result.
                Should()
                .NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Test_GetById_Returns_Ok()
        {
            //Realizando o cadastro de um estoque
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await httpClient.GetAsync(_endpoint + "/" + result.estoque.IdEstoque);

            var resposta = JsonConvert.DeserializeObject<Estoque>
               (response.Content.ReadAsStringAsync().Result);

            resposta
                .Should()
                .NotBeNull();
        }

        private StringContent CreateEstoqueData()
        {
            var faker = new Faker("pt_BR");

            var request = new EstoquePostRequest()
            {
                Nome = faker.Company.CompanyName(),
                Descricao = faker.Company.CompanyName()
            };

            return new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }
    }

    /// <summary>
    /// Classe para capturar o retorno do resultado dos testes POST, PUT ou DELETE
    /// </summary>
    public class EstoqueResult
    {
        public string message { get; set; }
        public Estoque estoque { get; set; }
    }
}



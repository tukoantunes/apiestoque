using ApiEstoque.Services.Requests;
using ApiEstoque.Tests.Config;
using Bogus;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiEstoque.Tests
{
    public class LoginTest
    {
        private readonly string _endpointLogin;
        private readonly string _endpointRegister;

        public LoginTest()
        {
            _endpointLogin = ApiConfig.GetEndpoint() + "/login";
            _endpointRegister = ApiConfig.GetEndpoint() + "/register";
        }

        [Fact]
        public async Task Test_Post_Returns_Ok()
        {
            var httpClient = new HttpClient();

            #region Cadastrando um usuário

            var faker = new Faker("pt_BR");

            var registerRequest = new RegisterPostRequest()
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email.ToLower(),
                Senha = faker.Internet.Password()
            };

            var registerContent = new StringContent
                (JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json");

            await httpClient.PostAsync(_endpointRegister, registerContent);

            #endregion

            #region Autenticando o usuário cadastrado

            var loginRequest = new LoginPostRequest()
            {
                Email = registerRequest.Email,
                Senha = registerRequest.Senha
            };

            var loginContent = new StringContent
                (JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_endpointLogin, loginContent);

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            #endregion

        }

        [Fact]
        public async Task Test_Post_Returns_Unauthorized()
        {
            var httpClient = new HttpClient();

            var faker = new Faker("pt_BR");

            var request = new LoginPostRequest()
            {
                Email = faker.Person.Email.ToLower(),
                Senha = faker.Internet.Password()
            };

            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_endpointLogin, content);

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);

        }
    }
}




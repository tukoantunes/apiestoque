using ApiEstoque.Services.Requests;
using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoque.Tests.Config
{
    public class AuthenticationConfig
    {
        //método para autenticar na API e retornar o TOKEN do usuário
        public async Task<string> ObterTokenAcesso()
        {
            var httpClient = new HttpClient();

            #region Criando um usuário na API

            var faker = new Faker("pt_BR");

            var registerRequest = new RegisterPostRequest()
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email.ToLower(),
                Senha = faker.Internet.Password()
            };

            var registerContent = new StringContent
                (JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json");

            await httpClient.PostAsync(ApiConfig.GetEndpoint() + "/register", registerContent);

            #endregion

            #region Autenticando o usuário

            var loginRequest = new LoginPostRequest()
            {
                Email = registerRequest.Email,
                Senha = registerRequest.Senha
            };

            var loginContent = new StringContent
                (JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ApiConfig.GetEndpoint() + "/login", loginContent);
            var result = JsonConvert.DeserializeObject<AuthenticationResult>
                (response.Content.ReadAsStringAsync().Result);

            return result.accessToken;

            #endregion
        }

    }

    //Classe para capturar o retorno do serviço de autenticação
    public class AuthenticationResult
    {
        //conteudo do TOKEN do usuário
        public string accessToken { get; set; }
    }
}



using System.Net.Http;

namespace Contatos.Services
{
    public abstract class ServiceBase
    {
        // Acessar os serviços
        protected HttpClient cliente;

        // Armazena o resultado da execução
        public ResultadoOperacao Resultado { get; set; }

        public ServiceBase()
        {
            // Instanciar o cliente
            cliente = new HttpClient();
            // Definir o tamanho do buffer
            cliente.MaxResponseContentBufferSize = 256000;
        }

        protected void AdicionarToken()
        {
            var token = "";
            token = "de880996-93a5-4f63-a8a8-7bfa9a577704";

            // Definir o token no cabeçalho da requisição
            cliente.DefaultRequestHeaders.Add("token", token);
        }
    }
}

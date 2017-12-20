using Contatos.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Contatos.Services
{
    public class EnderecoService : ServiceBase
    {
        public EnderecoService() : base()
        {
            cliente.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        // Método para pesquisar o cep
        public async Task<Endereco> Pesquisar(string cep)
        {
            // Declarar o objeto de retorno
            Endereco end = null;

            // Inicializar o resultado da chamada do serviço
            Resultado = new ResultadoOperacao()
            {
                Sucesso = true
            };

            // Criar o parâmetro para a consulta
            var parametro = cep + "/json";

            try
            {
                // Chamar o serviço
                var resposta = await cliente.GetAsync(parametro);

                // Ler o conteúdo da resposta
                var conteudo = await resposta.Content.ReadAsStringAsync();

                // Verificar se executou com sucesso
                if (resposta.IsSuccessStatusCode)
                {
                    // Verificar se o serviço NÃO encontrou o cep
                    if (!conteudo.Contains("\"erro\": true"))
                    {
                        // Desserializar o endereço
                        end = JsonConvert.DeserializeObject<Endereco>(conteudo);
                    }
                    else
                    {
                        // Se não localizou o cep
                        Resultado.Sucesso = false;
                        Resultado.Mensagem = "CEP (" + cep + ") não localizado!";
                    }
                }
                else
                {
                    // Se a execução NÃO foi realizada com sucesso.
                    Resultado.Sucesso = false;
                    Resultado.Mensagem = conteudo;
                    Resultado.NumeroErro = (int)resposta.StatusCode;
                }
            }
            catch(Exception ex)
            {
                // Se ocorreu erro na execução
                Resultado.Sucesso = false;
                Resultado.Mensagem = ex.Message;
                Resultado.NumeroErro = ex.HResult;
            }

            return end;
        }
    }
}

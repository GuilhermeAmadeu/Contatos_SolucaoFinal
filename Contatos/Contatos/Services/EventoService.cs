using Contatos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Services
{
    public class EventoService : ServiceBase
    {
        public EventoService() : base()
        {
            // Definir o endereço do cliente
            cliente.BaseAddress = new Uri(string.Format(Constantes.URL_SERVICO, "eventoapi"));
        }

        public async Task<List<Evento>> PesquisarAsync(string conteudo)
        {
            // Itens que serão retornados pela pesquisa
            var itens = new List<Evento>();

            // Instanciar o resultado da operação
            Resultado = new ResultadoOperacao()
            {
                Sucesso = false
            };

            // Criar o parâmetro para chamada
            var parametro = "?content=" + conteudo;

            try
            {
                // Chamar o serviço
                var resposta = await cliente.GetAsync(parametro);

                // Verificar se executou com sucesso
                if (resposta.IsSuccessStatusCode)
                {
                    // Obter o conteúdo da resposta
                    var conteudoret = await resposta.Content.ReadAsStringAsync();

                    // Transformar o conteúdo retornado na lista de itens
                    itens = JsonConvert.DeserializeObject<List<Evento>>(conteudoret);

                    // Assinalar o resultado como sucesso
                    Resultado.Sucesso = true;
                }
                else
                {
                    // Obter a mensagem de erro
                    Resultado.Mensagem = await resposta.Content.ReadAsStringAsync();
                    Resultado.NumeroErro = (int)resposta.StatusCode;
                }
            }
            catch(Exception ex)
            {
                // Obter a mensagem de erro
                Resultado.Mensagem = ex.Message;
            }

            return itens;
        }

        public async Task SalvarAsync(Evento item, bool ehNovo = false)
        {
            // Inicializar o resultado da operação
            Resultado = new ResultadoOperacao()
            {
                Sucesso = false
            };

            try
            {
                // Converter os dados para json
                var json = JsonConvert.SerializeObject(item);

                // Criar o conteúdo para envio dos dados json
                var conteudo = new StringContent(json, Encoding.UTF8, Constantes.CONTEUDO_JSON);

                // Declarar o objeto de resposta http da requisição
                HttpResponseMessage resposta = null;

                // Verificar se é um item para inclusão
                if (ehNovo == true)
                {
                    resposta = await cliente.PostAsync("", conteudo);
                }
                else
                {
                    resposta = await cliente.PutAsync("", conteudo);
                }

                // Verificar se houve sucesso na chamada
                if (resposta.IsSuccessStatusCode)
                {
                    Resultado.Sucesso = true;
                }
                else
                {
                    // Obter as informações da requisição
                    Resultado.Mensagem = await resposta.Content.ReadAsStringAsync();
                    Resultado.NumeroErro = (int)resposta.StatusCode;
                }
            }
            catch(Exception ex)
            {
                Resultado.Mensagem = ex.Message;
            }
        }

        public async Task ExcluirAsync(Evento item)
        {
            // Inicializar o resultado da operação
            Resultado = new ResultadoOperacao()
            {
                Sucesso = false
            };

            // Criar o parâmetro para requisição
            var parametro = "?id=" + item.Id.ToString();

            try
            {
                
                var resposta = await cliente.DeleteAsync(parametro);

                // Verificar se houve sucesso na chamada
                if (resposta.IsSuccessStatusCode)
                {
                    Resultado.Sucesso = true;
                }
                else
                {
                    // Obter as informações da requisição
                    Resultado.Mensagem = await resposta.Content.ReadAsStringAsync();
                    Resultado.NumeroErro = (int)resposta.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Resultado.Mensagem = ex.Message;
            }
        }
    }
}

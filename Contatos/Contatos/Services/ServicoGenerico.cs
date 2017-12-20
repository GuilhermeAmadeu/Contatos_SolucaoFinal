using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Services
{
    public class ServicoGenerico<T> where T : class
    {
        private HttpClient cliente;

        public ResultadoOperacao Status { get; set; }

        public ServicoGenerico(string enderecoBase)
        {
            cliente = new HttpClient();
            cliente.MaxResponseContentBufferSize = 256000;
            cliente.BaseAddress = new Uri(enderecoBase);

            //Define o Header de resultado para JSON, para evitar que seja retornado um HTML ou XML
            //cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void AdicionarToken()
        {
            string token = SessaoHelper.SelecionarToken();

            cliente.DefaultRequestHeaders.Add("token", token);
        }

        public async Task<string> PesquisarAsync(string parametro)
        {
            string resultado = null;

            Status = new ResultadoOperacao()
            {
                Sucesso = false
            };

            try
            {
                var resposta = await cliente.GetAsync(parametro);
                var conteudo = await resposta.Content.ReadAsStringAsync();

                if (resposta.IsSuccessStatusCode)
                {
                    resultado = conteudo;

                    Status.Sucesso = true;
                }
                else
                {
                    Status.Mensagem = conteudo;
                    Status.NumeroErro = (int)resposta.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Status.Mensagem = ex.Message;

                //Debug.WriteLine(Constantes.MENSAGEM_ERRO, ex.Message);
            }

            return resultado;
        }

        public async Task<string> SalvarAsync(T item, bool ehNovo = false)
        {
            string resultado = null;

            Status = new ResultadoOperacao()
            {
                Sucesso = false
            };

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var dadosenvio = new StringContent(json, Encoding.UTF8, Constantes.CONTEUDO_JSON);

                HttpResponseMessage resposta = null;
                if (ehNovo)
                {
                    resposta = await cliente.PostAsync("", dadosenvio);
                }
                else
                {
                    resposta = await cliente.PutAsync("", dadosenvio);
                }

                var conteudo = await resposta.Content.ReadAsStringAsync();

                if (resposta.IsSuccessStatusCode)
                {
                    resultado = conteudo;

                    Status.Sucesso = true;

                    //Debug.WriteLine(Constantes.MENSAGEM_SALVO_SUCESSO);
                }
                else
                {
                    Status.Mensagem = conteudo;
                    Status.NumeroErro = (int)resposta.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Status.Mensagem = ex.Message;

                //Debug.WriteLine(Constantes.MENSAGEM_ERRO_SALVAR, ex.Message);
            }

            return resultado;
        }

        public async Task<string> ExcluirAsync(string parametro)
        {
            string resultado = null;

            Status = new ResultadoOperacao()
            {
                Sucesso = false
            };

            try
            {
                var resposta = await cliente.DeleteAsync(parametro);

                var conteudo = await resposta.Content.ReadAsStringAsync();

                if (resposta.IsSuccessStatusCode)
                {
                    resultado = conteudo;

                    Status.Sucesso = true;

                    //Debug.WriteLine(Constantes.MENSAGEM_SALVO_SUCESSO);
                }
                else
                {
                    Status.Mensagem = conteudo;
                    Status.NumeroErro = (int)resposta.StatusCode;
                }

            }
            catch (Exception ex)
            {
                Status.Mensagem = ex.Message;

                //Debug.WriteLine(Constantes.MENSAGEM_ERRO_EXCLUIR, ex.Message);
            }

            return resultado;
        }
    }
}
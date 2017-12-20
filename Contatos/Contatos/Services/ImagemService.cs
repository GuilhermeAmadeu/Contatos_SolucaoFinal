using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Contatos.Services
{
    public class ImagemService : ServiceBase
    {
        public ImagemService() : base()
        {
            // Adicionar o token para a requisição
            AdicionarToken();

            // Definir o endereço do cliente
            cliente.BaseAddress = new Uri(Constantes.URL_SERVICO + "imagemapi");
        }

        public async Task<ResultadoOperacao> EnviarFoto(string nomeArquivo,
            Stream foto)
        {
            // Método para enviar a foto para o servidor

            Resultado = new ResultadoOperacao()
            {
                Sucesso = false
            };

            // Executar a ação
            try
            {
                // Criar o conteúdo para envio
                var dadosenvio = new MultipartFormDataContent();

                //var teste = @"c:\caminho\arquivo.txt";

                string nome = "\"Perfil\"";
                string arquivo = string.Format("\"{0}\"", nomeArquivo);

                var conteudo = new StreamContent(foto);

                dadosenvio.Add(conteudo, nome, arquivo);

                // Chamar o serviço para realizar o upload
                await cliente.PostAsync("", dadosenvio).ContinueWith(task =>
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            // Obter a resposta
                            var resposta = task.Result;

                            // Verificar se executou com sucesso
                            if (resposta.IsSuccessStatusCode == true)
                            {
                                Resultado.Sucesso = true;
                            }
                            else
                            {
                                Resultado.NumeroErro = (int)resposta.StatusCode;
                                Resultado.Mensagem = string.Format("{0} - ({1})",
                                    resposta.Content.ReadAsStringAsync().Result,
                                    resposta.ReasonPhrase);
                            }
                        }
                    });
            }
            catch(Exception ex)
            {
                Resultado.Mensagem = ex.Message;
            }

            return Resultado;
        }

        public async Task<Stream> ObterFoto(string nomeArquivo)
        {
            Resultado = new ResultadoOperacao()
            {
                Sucesso = false
            };

            // OU
            //Resultado = new ResultadoOperacao();
            //Resultado.Sucesso = false;

            string parametro = "?filename=" + nomeArquivo;

            Stream fotostream = null;

            try
            {
                // Chamar o serviço para realizar o download
                await cliente.GetAsync(parametro).ContinueWith(async task => 
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            // Obter a resposta
                            var resposta = task.Result;

                            // Verificar se executou com sucesso
                            if (resposta.IsSuccessStatusCode == true)
                            {
                                // Obter o stream do arquivo
                                fotostream = await resposta.Content.
                                    ReadAsStreamAsync();

                                Resultado.Sucesso = true;
                            }
                            else
                            {
                                Resultado.NumeroErro = (int)resposta.StatusCode;
                                Resultado.Mensagem = string.Format("{0} - ({1})",
                                    resposta.Content.ReadAsStringAsync().Result,
                                    resposta.ReasonPhrase);
                            }
                        }
                    });
            }
            catch(Exception ex)
            {
                Resultado.Mensagem = ex.Message;
            }

            return fotostream;
        }
    }
}

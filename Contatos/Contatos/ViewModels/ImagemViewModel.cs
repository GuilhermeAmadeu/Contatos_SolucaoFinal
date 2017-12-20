using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contatos.Services;
using System.IO;
using Contatos.Helpers;

namespace Contatos.ViewModels
{
    public class ImagemViewModel
    {
        private ImagemService srv;

        public ImagemViewModel()
        {
            srv = new ImagemService();
        }

        public async Task<ResultadoOperacao> EnviarFoto(string nomeArquivo,
            Stream foto)
        {
            var resultado = await srv.EnviarFoto(nomeArquivo, foto);

            return resultado;
        }

        public async Task<Stream> ObterFoto(string nomeArquivo)
        {
            Stream fotostream = null;

            bool existe = await ArmazenamentoHelper.ExisteArquivoAsync(nomeArquivo);

            if (existe == false)
            {
                fotostream = await srv.ObterFoto(nomeArquivo);

                if (srv.Resultado.Sucesso == true)
                {
                    // Salvar o arquivo local
                    await ArmazenamentoHelper.ConverterStreamArquivoAsync(nomeArquivo, null, fotostream);
                }
            }
            else
            {
                fotostream = await ArmazenamentoHelper.ConverterArquivoStreamAsync(nomeArquivo, null);
            }

            return fotostream;
        }

        public string ObterNomeComExtensao(string id)
        {
            var nome = id + ".jpg";

            return nome;
        }
    }
}

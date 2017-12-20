using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PCLStorage;

namespace Contatos.Helpers
{
    public static class ArmazenamentoHelper
    {
        // Método que verifica se existe um arquivo
        public async static Task<bool> ExisteArquivoAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            // Verifica se o arquivo existe na pasta
            ExistenceCheckResult pastaExiste = await pasta.CheckExistsAsync(nomeArquivo);
            // Verifica o ExistenceCheckResult
            if (pastaExiste == ExistenceCheckResult.FileExists)
            {
                return true;
            }

            return false;
        }

        // Metodo que verifica se existe uma pasta
        public async static Task<bool> ExistePastaAsync(string nomePasta, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult pastaExiste = await pasta.CheckExistsAsync(nomePasta);
            // Verifica se existe a pasta
            if (pastaExiste == ExistenceCheckResult.FolderExists)
            {
                return true;
            }

            return false;
        }

        // Metodo para salvar um arquivo
        public async static Task<IFile> SalvarArquivoAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            IFile arquivo = await pasta.CreateFileAsync(nomeArquivo, CreationCollisionOption.ReplaceExisting);
            return arquivo;
        }

        // Metodo para salvar uma pasta
        public async static Task<IFolder> SalvarPastaAsync(string nomePasta, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            pasta = await pasta.CreateFolderAsync(nomePasta, CreationCollisionOption.FailIfExists);
            return pasta;
        }

        // Metodo para apagar um arquivo
        public async static Task<bool> ApagarArquivoAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            // Verifica se existe o arquivo para ser apagado
            var existeArquivo = await ExisteArquivoAsync(nomeArquivo, pasta);
            if (existeArquivo)
            {
                // Seleciona o arquivo pelo nome
                IFile arquivo = await pasta.GetFileAsync(nomeArquivo);
                // Apaga o arquivo selecionado
                await arquivo.DeleteAsync();

                return true;
            }
            return false;
        }

        // Metodo para escreve em um arquivo
        public async static Task<bool> EscreverArquivoAsync(string nomeArquivo, IFolder pastaRoot = null, string texto = "")
        {
            IFile arquivo = await SalvarArquivoAsync(nomeArquivo, pastaRoot);
            await arquivo.WriteAllTextAsync(texto);
            return true;
        }

        // Metodo para ler um arquivo
        public async static Task<string> LerArquivoAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Valor do texto default
            string texto = string.Empty;
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            var existe = await ExisteArquivoAsync(nomeArquivo, pasta);
            if (existe)
            {
                IFile arquivo = await pasta.GetFileAsync(nomeArquivo);
                // Leitura do arquivo
                texto = await arquivo.ReadAllTextAsync();
            }
            return texto;
        }


        // Metodo que seleciona um arquivo
        public async static Task<IFile> PegarArquivoAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Valor do texto default
            IFile arquivo = null;
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            var existe = await ExisteArquivoAsync(nomeArquivo, pasta);
            if (existe)
            {
                arquivo = await pasta.GetFileAsync(nomeArquivo);
            }
            return arquivo;
        }

        // Metodo de conversao de arquivo em byte
        public async static Task<byte[]> ConverterArquivoByteAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            IFile arquivo = await PegarArquivoAsync(nomeArquivo, pasta);
            // Passagem do arquivo em memoria
            using (Stream streamArquivo = await arquivo.OpenAsync(FileAccess.ReadAndWrite))
            {
                // Conversao de byte
                long tamanhoBuffer = streamArquivo.Length;
                byte[] streamBuffer = new byte[tamanhoBuffer];
                streamArquivo.Read(streamBuffer, 0, (int)tamanhoBuffer);
                return streamBuffer;
            }
        }

        // Metodo de conversao de byte em arquivo
        public async static Task<IFile> ConverterByteArquivoAsync(string nomeArquivo, IFolder pastaRoot = null, byte[] dados = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            IFile arquivo = await SalvarArquivoAsync(nomeArquivo, pasta);
            // Passagem do arquivo em memoria
            using (Stream streamArquivo = await arquivo.OpenAsync(FileAccess.ReadAndWrite))
            {
                streamArquivo.Write(dados, 0, dados.Length);
            }
            return arquivo;
        }

        // Metodo de conversao de arquivo para stream(memoria)
        public async static Task<Stream> ConverterArquivoStreamAsync(string nomeArquivo, IFolder pastaRoot = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            // Abre o arquivo
            IFile arquivo = await PegarArquivoAsync(nomeArquivo, pasta);
            return await arquivo.OpenAsync(FileAccess.Read);
        }

        // Metodo de conversao de stream para arquivo
        public async static Task<IFile> ConverterStreamArquivoAsync(string nomeArquivo, IFolder pastaRoot = null, Stream dadosMemoria = null)
        {
            // Acessar o sistema de arquivos Root
            IFolder pasta = pastaRoot ?? FileSystem.Current.LocalStorage;
            IFile arquivo = await SalvarArquivoAsync(nomeArquivo, pasta);
            using (Stream streamArquivo = await arquivo.OpenAsync(FileAccess.ReadAndWrite))
            {
                await dadosMemoria.CopyToAsync(streamArquivo);
            }
            return arquivo;
        }



    }
}

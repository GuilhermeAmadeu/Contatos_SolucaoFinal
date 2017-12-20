using Contatos.iOS;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Contatos.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            // Obter a pasta pessoal
            var personalfolder = Environment.GetFolderPath(
                Environment.SpecialFolder.Personal);

            // Concatenar com a pasta library
            var folder = Path.Combine(personalfolder, "..", "Library");

            // Verificar se a pasta existe
            if (!Directory.Exists(folder))
            {
                // Criar a pasta
                Directory.CreateDirectory(folder);
            }

            // Retornar o caminho do arquivo
            return Path.Combine(folder, fileName);
        }
    }
}
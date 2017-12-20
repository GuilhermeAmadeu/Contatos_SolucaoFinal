using Contatos.Droid;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Contatos.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            // Obter a pasta da aplicação
            var folder = Environment.GetFolderPath(
                Environment.SpecialFolder.Personal);

            // Retornar o caminho do arquivo
            return Path.Combine(folder, fileName);
        }
    }
}
using Contatos.UWP;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Contatos.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            // Obter a pasta
            var folder = ApplicationData.Current.LocalFolder.Path;

            // Retornar o caminho do arquivo
            return Path.Combine(folder, fileName);
        }
    }
}

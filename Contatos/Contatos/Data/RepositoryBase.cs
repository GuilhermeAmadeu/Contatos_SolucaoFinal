using Contatos.Models;
using SQLite;
using Xamarin.Forms;

namespace Contatos.Data
{
    public class RepositoryBase
    {
        protected static SQLiteAsyncConnection Conexao { get; set; }

        public RepositoryBase()
        {
            // Verificar se a conexão está nula
            if (Conexao == null)
            {
                Inicializar();
            }
        }

        private void Inicializar()
        {
            // Obter a implementação da interface de acordo com a app
            IFileHelper fh = DependencyService.Get<IFileHelper>();

            // Caminho do arquivo do banco
            var caminho = fh.GetLocalFilePath("ContatosSQLite.db3");

            // Instanciar a conexão
            Conexao = new SQLiteAsyncConnection(caminho);

            // Criar tabelas
            Conexao.CreateTableAsync<Usuario>().Wait();
            Conexao.CreateTableAsync<Pessoa>().Wait();
            Conexao.CreateTableAsync<Evento>().Wait();
        }
    }
}

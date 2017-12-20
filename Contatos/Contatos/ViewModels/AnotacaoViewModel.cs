using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contatos.Models;
using Contatos.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Contatos.ViewModels
{
    public class AnotacaoViewModel : ViewModelBase
    {
        // Declarar o objeto do serviço genérico
        private ServicoGenerico<Anotacao> srv;


        private ObservableCollection<Anotacao> lista;
        public ObservableCollection<Anotacao> Lista
        {
            get
            {
                return lista;
            }
            set
            {
                lista = value;
                OnPropertyChanged();
            }
        }

        public string ConteudoPesquisa { get; set; }

        // Criar o construtor
        public AnotacaoViewModel()
        {
            string url = Constantes.URL_SERVICO + "anotacaoapi/";

            // Acessar o serviço online
            //url = "http://contatosedu.azurewebsites.net/api/anotacaoapi/";

            // Instanciar o serviço genérico
            srv = new ServicoGenerico<Anotacao>(url);

            // Instanciar a lista
            Lista = new ObservableCollection<Anotacao>();
        }

        public async Task PesquisarAsync()
        {
            string parametro = "?content=" + ConteudoPesquisa;

            // Executar o método Get
            var retorno = await srv.PesquisarAsync(parametro);

            // Verificar se foi executado com sucesso
            if (srv.Status.Sucesso == true)
            {
                var itens = JsonConvert.DeserializeObject<List<Anotacao>>(retorno);
                Lista = new ObservableCollection<Anotacao>(itens);
            }
        }

        public async Task<ResultadoOperacao> SalvarAsync(Anotacao item)
        {
            // Verificar se é um novo registro
            bool novo = (item.Id == 0);

            // Obter o resultado da rotina de salvar
            string retorno = await srv.SalvarAsync(item, novo);

            // Obter o resultado da execução
            ResultadoOperacao resultado = srv.Status;

            // Verificar se o serviço foi executado com sucesso
            if (resultado.Sucesso == true)
            {
                Anotacao itemsalvo = JsonConvert.
                    DeserializeObject<Anotacao>(retorno);

                if (novo == true)
                {
                    Lista.Add(itemsalvo);
                }
            }

            return resultado;
        }

        public async Task<ResultadoOperacao> ExcluirAsync(Anotacao item)
        {
            // Criar o parametro
            string parametro = "?id=" + item.Id;

            // Executar a exclusão
            string retorno = await srv.ExcluirAsync(parametro);

            // Obter o resultado da execução
            ResultadoOperacao resultado = srv.Status;

            // Verificar se foi executado com sucesso
            if (resultado.Sucesso == true)
            {
                Lista.Remove(item);
            }

            return resultado;
        }

        public Anotacao Novo()
        {
            Anotacao item = new Anotacao();

            return item;
        }
    }
}

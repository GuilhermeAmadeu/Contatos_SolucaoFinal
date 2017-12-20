using Contatos.Data;
using Contatos.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Contatos.ViewModels
{
    public class EventoViewModel : ViewModelBase
    {
        // Criar o repositório
        private EventoRepository rep = new EventoRepository();

        // Criar campo da lista
        private ObservableCollection<Evento> lista = 
            new ObservableCollection<Evento>();

        // Criar a propriedade da lista
        public ObservableCollection<Evento> Lista
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

        // Armazena o conteúdo para pesquisa
        public string ConteudoPesquisa { get; set; }

        // Método para instanciar novo item
        public Evento Novo()
        {
            var item = new Evento();

            return item;
        }

        // Método para realizar a pesquisa
        public async Task Pesquisar()
        {
            // Obet as pessoas cadastradas
            var itens = await rep.PesquisarAsync(ConteudoPesquisa);

            Lista = new ObservableCollection<Evento>(itens);
        }

        // Método para salvar o item
        public async Task<ResultadoOperacao> Salvar(Evento item)
        {
            ResultadoOperacao resultado = null;

            // Verificar se é um novo item
            bool novo = false;
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
                novo = true;
            }

            // Salvar no repositório
            resultado = await rep.SalvarAsync(item);

            // Se executou com sucesso
            if (resultado.Sucesso)
            {
                // Se for novo item
                if (novo == true)
                {
                    Lista.Add(item);
                }

                // Continuará no futuro
            }

            return resultado;
        }

        // Método para excluir o item
        public async Task<ResultadoOperacao> Excluir(Evento item)
        {
            ResultadoOperacao resultado = null;

            // Excliuir do repositório
            resultado = await rep.ExcluirAsync(item);

            // Se houve sucesso na exclusão
            if (resultado.Sucesso)
            {
                // REmover da lista
                Lista.Remove(item);
            }

            return resultado;
        }
    }
}

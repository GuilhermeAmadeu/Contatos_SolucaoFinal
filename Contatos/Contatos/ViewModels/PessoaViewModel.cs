using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Contatos.Data;
using Contatos.Models;
using Contatos.Services;

namespace Contatos.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        // Criar o repositório
        private PessoaRepository rep = new PessoaRepository();
        private PessoaService srv = new PessoaService();

        // Criar campo da lista
        private ObservableCollection<Pessoa> lista = 
            new ObservableCollection<Pessoa>();

        // Criar a propriedade da lista
        public ObservableCollection<Pessoa> Lista
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
        public Pessoa Novo()
        {
            var item = new Pessoa()
            {
                IdUsuario = 1, // Modificar depois
                DataNascimento = DateTime.Today
            };

            return item;
        }

        // Método para realizar a pesquisa
        public async Task Pesquisar()
        {
            // Obet as pessoas cadastradas
            var itens = await rep.PesquisarAsync(ConteudoPesquisa);

            Lista = new ObservableCollection<Pessoa>(itens);
        }

        // Método para salvar o item
        public async Task<ResultadoOperacao> Salvar(Pessoa item)
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

                // Executar a chamada para o serviço
                await srv.SalvarAsync(item, novo);

                // Obter o resultado da execução
                resultado = srv.Resultado;
            }

            return resultado;
        }

        // Método para excluir o item
        public async Task<ResultadoOperacao> Excluir(Pessoa item)
        {
            ResultadoOperacao resultado = null;

            // Chamar o serviço para excluir
            await srv.ExcluirAsync(item);

            // Obter resultado da execução
            resultado = srv.Resultado;

            // Verificar se foi excluído com sucesso
            if (resultado.Sucesso)
            {
                // Excliuir do repositório
                resultado = await rep.ExcluirAsync(item);

                // Se houve sucesso na exclusão
                if (resultado.Sucesso)
                {
                    // REmover da lista
                    Lista.Remove(item);
                }
            }

            return resultado;
        }

        public async Task<ResultadoOperacao> SincronizarAsync()
        {
            ResultadoOperacao resultado = null;

            bool possuierro = false;

            // Criar a lista de ids processados
            var listaproc = new List<Guid>();

            // Obter a lista de itens on-line
            var listanuvem = await srv.PesquisarAsync("");

            // Verificar se executou com sucesso
            if (!srv.Resultado.Sucesso)
            {
                resultado = srv.Resultado;
                return resultado;
            }

            // Atualizar a lista local
            await Pesquisar();

            // Verificar se os itens locais estão na nuvem
            foreach (var itemlocal in Lista)
            {
                // Verificar se o item existe na lista da nuvem
                var itemnuvem = listanuvem.Where(r => r.Id == itemlocal.Id).FirstOrDefault();

                if (itemnuvem == null)
                {
                    // Executar a chamada para o serviço
                    await srv.SalvarAsync(itemlocal, true);

                    if (!srv.Resultado.Sucesso)
                        possuierro = true;
                }
                else
                {
                    // Remover da lista da nuvem
                    listanuvem.Remove(itemnuvem);
                }
            }

            // Verificar se os itens da nuvem estão locais
            foreach (var itemnuvem in listanuvem)
            {
                // Salvar no repositório
                resultado = await rep.SalvarAsync(itemnuvem);

                if (resultado.Sucesso)
                {
                    // Adicionar novo item na lista
                    Lista.Add(itemnuvem);
                }
                else
                {
                    possuierro = true;
                }
            }

            // Verificar se ocorreram erros durante a execução
            if (possuierro)
            {
                resultado = new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Ocorreram erros durante a sincronização."
                };
            }
            else
            {
                resultado = new ResultadoOperacao()
                {
                    Sucesso = true
                };
            }

            return resultado;
        }        
    }
}

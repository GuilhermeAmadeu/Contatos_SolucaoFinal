using Contatos.Models;
using Contatos.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnotacaoListaPage : ContentPage
    {
        private AnotacaoViewModel vm;

        public AnotacaoListaPage()
        {
            InitializeComponent();

            Inicializar();

            Pesquisar();
        }

        private void Inicializar()
        {
            // Instanciar a viewmodel
            vm = new AnotacaoViewModel();

            // Vincular o objeto com a página
            this.BindingContext = vm;
        }

        private async void Pesquisar()
        {
            await vm.PesquisarAsync();
        }

        private async void tbiNovo_Clicked(object sender, EventArgs e)
        {
            // Criar o objeto de binding
            var item = vm.Novo();

            // Criar a página de edição
            var pagina = new AnotacaoEdicaoPage();
            // Definir o binding
            pagina.BindingContext = item;
            // Atribuir os eventos
            pagina.Salvando += SalvarHandler;

            await App.NavegacaoPaginaAsync(pagina);
        }

        private void txtPesquisa_SearchButtonPressed(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private async void lvLista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Obter o objeto selecionado
            var item = (Anotacao)e.Item;

            // Criar a página de edição
            var pagina = new AnotacaoEdicaoPage();
            // Definir o binding
            pagina.BindingContext = item;
            // Associar os eventos
            pagina.Salvando += SalvarHandler;
            pagina.Excluindo += ExcluirHandler;

            // Chamar a página
            await Navigation.PushAsync(pagina);
        }

        private async void SalvarHandler(object sender, ItemEventArgs e)
        {
            // Realizar a conversão
            var item = (e.Item as Anotacao);

            var resultado = await vm.SalvarAsync(item);

            if (resultado.Sucesso)
            {
                await Navigation.PopAsync();
                await DisplayAlert("Sucesso", Constantes.MENSAGEM_SALVO_SUCESSO, "Fechar");
            }
            else
            {
                await DisplayAlert("Erro", string.Format(Constantes.MENSAGEM_ERRO_SALVAR, resultado.Mensagem), "Fechar");
            }
        }

        private async void ExcluirHandler(object sender, ItemEventArgs e)
        {
            // Solicitar confirmação
            var acao = await DisplayActionSheet("Confirma a exclusão?", "Cancelar", null, "Sim", "Não");

            if (acao != "Sim")
            {
                return;
            }

            // Realizar a conversão
            var item = (e.Item as Anotacao);

            var resultado = await vm.ExcluirAsync(item);

            if (resultado.Sucesso)
            {
                await Navigation.PopAsync();
                await DisplayAlert("Sucesso", Constantes.MENSAGEM_EXCLUIDO_SUCESSO, "Fechar");
            }
            else
            {
                await DisplayAlert("Erro", string.Format(Constantes.MENSAGEM_ERRO_EXCLUIR, resultado.Mensagem), "Fechar");
            }
        }
    }
}
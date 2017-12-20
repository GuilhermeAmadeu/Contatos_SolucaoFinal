using Contatos.Models;
using Contatos.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventoListaPage : ContentPage
    {
        private EventoViewModel vm;

        public EventoListaPage()
        {
            InitializeComponent();

            Inicializar();
        }

        private void Inicializar()
        {
            // Instanciar a viewmodel
            vm = new EventoViewModel();

            // Vincular a vm com a página
            BindingContext = vm;

            // Definir a fonte de dados da lista
            lvEvento.ItemsSource = vm.Lista;
        }

        private async void tbiNovo_Clicked(object sender, EventArgs e)
        {
            // Criar o objeto de binding
            var item = vm.Novo();

            // Criar a página de edição
            var pagina = new EventoEdicaoPage();
            // Definir o binding
            pagina.BindingContext = item;
            // Atribuir os eventos
            pagina.Salvando += SalvarHandler;

            // Chamar a página
            await Navigation.PushAsync(pagina);
        }

        private async void lvEvento_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Obter o objeto selecionado
            var item = (Evento)e.Item;

            // Criar a página de edição
            var pagina = new EventoEdicaoPage();
            // Definir o binding
            pagina.BindingContext = item;
            // Atribuir os eventos
            pagina.Salvando += SalvarHandler;
            pagina.Excluindo += ExcluirHandler;

            // Chamar a página
            await Navigation.PushAsync(pagina);
        }

        private async void SalvarHandler(object sender, ItemEventArgs e)
        {
            // Obter o item passado como parâmetro
            var item = (e.Item as Evento);

            // Executar a rotina de salvar
            var resultado = await vm.Salvar(item);

            // Verificar o resultado da execução
            if (resultado.Sucesso)
            {
                await DisplayAlert("Sucesso", "Operação realizada com sucesso!",
                    "Fechar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro", resultado.Mensagem, "Fechar");
            }
        }

        private async void ExcluirHandler(object sender, ItemEventArgs e)
        {
            // Obter o item passado como parâmetro
            var item = (e.Item as Evento);

            // Executar a rotina de excluir
            var resultado = await vm.Excluir(item);

            // Verificar o resultado da execução
            if (resultado.Sucesso)
            {
                await DisplayAlert("Sucesso", "Operação realizada com sucesso!",
                    "Fechar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro", resultado.Mensagem, "Fechar");
            }
        }
    }
}
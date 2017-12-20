using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Contatos.Pages;
using System.Threading.Tasks;

namespace Contatos
{
    public partial class App : Application
    {
        // Atributos globais
        // Atributo somente de leitura
        public const string nomeApp = "Contato"; 

        // Configura a página mestre detalhe ativa
        public static MasterDetailPage PaginaMestreDetalhe { get; set; }

        // Metodos de navegação da aplicação
        public static async Task NavegacaoPaginaAsync(Page pagina)
        {
            // Define o titulo com o nome da aplicação
            pagina.Title = nomeApp;
            // Fecha a página de menu
            App.PaginaMestreDetalhe.IsPresented = false;
            // Carrega a nova página a partir do inicio (página mestre detalhe)
            await PaginaMestreDetalhe.Detail.Navigation.PushAsync(pagina, true);
        }

        public static async Task NavegacaoPaginaAnteriorAsync()
        {
            // Fecha a página de menu
            App.PaginaMestreDetalhe.IsPresented = false;
            // Volta para a página anterior 
            await PaginaMestreDetalhe.Detail.Navigation.PopAsync(true);
        }

        public static async Task NavegacaoPaginaInicialAsync()
        {
            // Fecha a página de menu
            App.PaginaMestreDetalhe.IsPresented = false;
            await PaginaMestreDetalhe.Detail.Navigation.PopToRootAsync();
        }

        // Dialogos
        
        // Exibe uma mensagem de alerta em tela
        public static async Task DialogoAlerta(
            string titulo, 
            string mensagem,
            string cancelar)
        {
            await App.Current.MainPage.DisplayAlert(titulo, mensagem, cancelar);
        }

        // Exibe uma mensagem de alerta em tela e volta o resultado
        // Confirmar = true
        // Cancelar = false
        public static async Task<bool> DialogoAlerta(
            string titulo,
            string mensagem,
            string confirmar,
            string cancelar)
        {
            return await App.Current.MainPage.DisplayAlert(titulo, mensagem, confirmar, cancelar);
        }

        // Exibe um menu de opções 
        // Ao escolher volta a opção (texto) escolhido
        public static async Task<string> DialogoOpcoes(
            string titulo, 
            string cancela, 
            params string[] opoces)
        {
            return await App.Current.MainPage.DisplayActionSheet(titulo, cancela, null, opoces);
        }



        public App()
        {
            InitializeComponent();

            ////MainPage = new Contatos.MainPage();

            // Definir a página inicial da aplicação
            //MainPage = new Contatos.Pages.PessoaListaPage();
            //MainPage = new NavigationPage(new Contatos.Pages.PessoaListaPage());
            MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

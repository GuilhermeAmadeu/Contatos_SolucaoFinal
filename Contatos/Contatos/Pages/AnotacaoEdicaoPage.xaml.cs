using Contatos.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnotacaoEdicaoPage : ContentPage
    {
        public EventHandler<ItemEventArgs> Salvando { get; set; }
        public EventHandler<ItemEventArgs> Excluindo { get; set; }

        public AnotacaoEdicaoPage()
        {
            InitializeComponent();
        }

        private void tbiExcluir_Clicked(object sender, EventArgs e)
        {
            // Fazer a operação de conversão
            Anotacao item = (Anotacao)this.BindingContext;

            // Executar o evento de excluir
            Excluindo?.Invoke(sender, new ItemEventArgs(item));
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            // Fazer a operação de conversão
            Anotacao item = (Anotacao)this.BindingContext;

            // Executar o evento de salvar
            Salvando?.Invoke(sender, new ItemEventArgs(item));
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            // Retornar para a página anterior
            await Navigation.PopAsync();
        }
    }
}
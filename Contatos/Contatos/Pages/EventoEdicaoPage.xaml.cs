using Contatos.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventoEdicaoPage : ContentPage
	{
        // Declarar os eventos
        public EventHandler<ItemEventArgs> Salvando { get; set; }
        public EventHandler<ItemEventArgs> Excluindo { get; set; }


        public EventoEdicaoPage ()
		{
			InitializeComponent ();
            Inicializar();
        }

        private void Inicializar()
        {

        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            // Fazer a operação de conversão
            Evento item = (Evento)this.BindingContext;

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
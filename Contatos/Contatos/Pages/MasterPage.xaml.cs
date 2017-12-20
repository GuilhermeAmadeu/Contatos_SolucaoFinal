using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();

            // Configuração da Página Mestre Detalhe
            // Página de navegação (Master)
            this.Master = new MenuPage() { Title = "Menu" };
            // Página de detalhe (Detail)
            this.Detail = new NavigationPage(new HomePage() { Title = App.nomeApp });
            // Seta o comportamento de exibição para o tipo PopOver (abre e fecha) do menu
            this.MasterBehavior = MasterBehavior.Popover;
            // Define como página mestre detalhe ativa
            App.PaginaMestreDetalhe = this; 

        }
    }
}
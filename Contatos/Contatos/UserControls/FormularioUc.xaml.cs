using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioUc : ContentView
    {

        // Propriedades
        // do formulario
        public string Titulo
        {
            get { return lblTitulo.Text; }
            set { lblTitulo.Text = value; }
        }

        public View Cabecalho
        {
            get { return cvCabecalhoItens.Content; }
            set { cvCabecalhoItens.Content = value; }
        }

        public View Conteudo
        {
            get { return cvConteudo.Content; }
            set { cvConteudo.Content = value; }
        }

        public View Rodape
        {
            get { return cvRodape.Content; }
            set { cvRodape.Content = value; }
        }

        public FormularioUc()
        {
            InitializeComponent();
        }
    }
}
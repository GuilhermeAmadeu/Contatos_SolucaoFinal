using Contatos.Models;
using Contatos.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Contatos.Helpers;
using Contatos.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PessoaEdicaoPage : ContentPage
    {
        private string nomearquivo;
        private ImagemViewModel ivm;

        // Declarar os eventos
        public EventHandler<ItemEventArgs> Salvando { get; set; }
        public EventHandler<ItemEventArgs> Excluindo { get; set; }

        public PessoaEdicaoPage()
        {
            InitializeComponent();

            Inicializar();
        }

        private void Inicializar()
        {
            ivm = new ImagemViewModel();
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            // Fazer a operação de conversão
            Pessoa item = (Pessoa)this.BindingContext;

            // Executar o evento de salvar
            Salvando?.Invoke(sender, new ItemEventArgs(item));
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            // Retornar para a página anterior
            await Navigation.PopAsync();
        }

        private async void txtCep_Unfocused(object sender, FocusEventArgs e)
        {
            // Verificar a quantidade de caracteres do cep
            if (txtCep.Text.Length < 8)
            {
                return;
            }

            // Instanciar o serviço de endereco
            var es = new EnderecoService();

            var endereco = await es.Pesquisar(txtCep.Text);

            // Verificar se retornou com sucesso
            if (es.Resultado.Sucesso == true)
            {
                txtEndereco.Text = endereco.Logradouro;
                txtBairro.Text = endereco.Bairro;
                txtCidade.Text = endereco.Localidade;
                txtUf.Text = endereco.Uf;
            }
            else
            {
                // Exibir mensagem de erro
                await DisplayAlert("Erro", es.Resultado.Mensagem, "Fechar");
            }
        }

        private void tbiExcluir_Clicked(object sender, EventArgs e)
        {
            // Chamar o evento de exclusão
            Excluindo?.Invoke(sender, new ItemEventArgs(BindingContext));
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            var modalCameraPage = new CameraPage(nomearquivo);
            modalCameraPage.eventoTrocar += (retorno, argumento) =>
            {
                string caminho = retorno.ToString();

                imgFoto.Source = ImageSource.FromFile(caminho);

                CarregarFoto(caminho);
            };

            // Verifica a opção selecionada
            //await App.NavegacaoPaginaAsync(new CameraPage());
            await Navigation.PushModalAsync(modalCameraPage);
        }

        private async void CarregarFoto(string caminho)
        {
            var nome = Path.GetFileName(caminho);

            var foto = await ArmazenamentoHelper
                .ConverterArquivoStreamAsync(nome);

            await ivm.EnviarFoto(nomearquivo, foto);
        }

        private async Task ExibirFoto()
        {
            var foto = await ivm.ObterFoto(nomearquivo);

            if (foto == null)
            { 
                imgFoto.Source = ImageSource.FromResource(
                    "Contatos.Resources.Imagens.usuario.png");
            }
            else
            {
                imgFoto.Source = ImageSource.FromStream(() => foto);
            }
        }

        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Pessoa item = BindingContext as Pessoa;

            nomearquivo = ivm.ObterNomeComExtensao(item.Id.ToString());

            await ExibirFoto();
        }
    }
}
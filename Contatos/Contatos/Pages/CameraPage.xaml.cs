using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Contatos.Helpers;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        private string nomearquivo = "Foto.jpg";

        //Formas de retorno para a pagina que chamou (pai)
        //public MediaFile ArquivoMidia { get; set; }
        public event EventHandler eventoTrocar;

        // Arquivo de midia
        MediaFile arquivoMidia; 

        //Metodo construtor
        public CameraPage(string nomeArquivo)
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(nomeArquivo))
            {
                //App.DialogoAlerta("Atenção", "Recebi esse dado:" + parametro, "Fechar");
                this.nomearquivo = nomeArquivo;
            }
        }

        public CameraPage()
        {
            InitializeComponent();
        }

        private void tprVoltar_Tapped(object sender, EventArgs e)
        {

        }

        private async void tprCamera_Tapped(object sender, EventArgs e)
        {
            // Pega a foto tirada na camera
            arquivoMidia = await CameraHelper.TirarFotoAsync(nomearquivo);
            // Executa o comando de exibir
            ExibirFotoAsync();
        }

        private async void tprAlbum_Tapped(object sender, EventArgs e)
        {
            // Pega a foto que esta no album da camera
            arquivoMidia = await CameraHelper.PegarFotoAsync();
            // Executa o comando de exibir
            ExibirFotoAsync();

        }

        private async void tprTrocar_Tapped(object sender, EventArgs e)
        {
            // Verifica se existe uma midia
            if (arquivoMidia == null)
            {
                await App.DialogoAlerta("Atenção", "Não foi selecionada uma foto", "Fechar");
                return;
            }

            // Se pagina pai chamar o evento vai execurtar e passar o valor com a foto
            eventoTrocar?.Invoke(arquivoMidia.Path, EventArgs.Empty);

            // Tirar da memoria o recurso
            arquivoMidia.Dispose();

            // Voltar para pagina principal
            //await App.NavegacaoPaginaInicialAsync();
            await Navigation.PopModalAsync();


        }

        // Comando que exibe a foto selecionada
        private async void ExibirFotoAsync()
        {
            // Verifica se existe uma foto para exibição
            if (arquivoMidia != null)
            {
                // Armazena o caminho onde esta foto tirada
                //string caminhoFoto = arquivoMidia.Path;
                // var streamFoto = arquivoMidia.GetStream();

                // Converte a foto armazenada na pasta para formato de memoria
                imgFoto.Source = ImageSource.FromStream(() =>
                {
                    var stream = arquivoMidia.GetStream();
                    return stream;
                });
                //imgFoto.Source = ImageSource.FromUri(caminhoFoto);
            }
            else
            {
                await App.DialogoAlerta("Atenção", "Não foi possivel selecionar a foto", "Fechar");
                return;
            }
        }
    }
}
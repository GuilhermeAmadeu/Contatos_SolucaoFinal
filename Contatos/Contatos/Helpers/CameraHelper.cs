using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace Contatos.Helpers
{
    public static class CameraHelper
    {
        public static async Task<MediaFile> TirarFotoAsync(string nomeArquivo)
        {
            // Inicaliza a camera do dispostivo
            await CrossMedia.Current.Initialize();

            // Verifica se existe uma camera e esta preparada para tirar fotos
            if (!CrossMedia.Current.IsCameraAvailable || 
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.DialogoAlerta("Atenção", "Camera não suportada", "Fechar");
                return null;
            }

            // Verifica se foi informado um nome para o arquivo
            if (string.IsNullOrWhiteSpace(nomeArquivo))
            {
                nomeArquivo = Guid.NewGuid().ToString();
                nomeArquivo += ".jpg";
            }

            // Armazena a foto tirada
            var midia = new StoreCameraMediaOptions();
            // Salva no album de foto do dispositivo
            midia.SaveToAlbum = true;
            midia.CompressionQuality = 60;
            midia.PhotoSize = PhotoSize.Medium;
            midia.Name = nomeArquivo;

            // Salva e retorna o arquivo de foto
            var foto = await CrossMedia.Current.TakePhotoAsync(midia);
            return foto;
        }

        public static async Task<MediaFile> PegarFotoAsync()
        {
            //Inicializa os recursos da camera
            await CrossMedia.Current.Initialize();

            // Verifica se permite a seleção de fotos da camera
            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.DialogoAlerta("Atenção", "Não permite selecionar fotos da camera", "Fechar");
                return null;
            }

            // Pegar a foto da galeria da camera
            return await CrossMedia.Current.PickPhotoAsync();
        }
    }
}

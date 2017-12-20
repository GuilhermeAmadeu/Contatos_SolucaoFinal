using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contatos.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para registrar a alteração da propriedade
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Disparar o evento
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

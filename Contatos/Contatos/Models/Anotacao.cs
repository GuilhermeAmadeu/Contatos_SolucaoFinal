using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contatos.Models
{

    /**********************************************
        Não criar esta classe
        Deve ser utilizada para exemplo complementar
    **********************************************/

    public class Anotacao : INotifyPropertyChanged
    {
        private string responsavel;
        private string descricao;

        [PrimaryKey()]
        public int Id { get; set; }


        public string Responsavel
        {
            get
            {
                return responsavel;
            }
            set
            {
                responsavel = value;
                NotifyPropertyChanged();
            }
        }

        public string Descricao
        {
            get
            {
                return descricao;
            }
            set
            {
                descricao = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

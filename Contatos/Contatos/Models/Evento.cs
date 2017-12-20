using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contatos.Models
{
    public class Evento : INotifyPropertyChanged
    {
        // Declarar os campos internos
        private string nome;
        private string local;
        private DateTime datahorainicio;
        private DateTime datahoratermino;
        private string status;
        private Guid idparticipante;
        private string anotacoes;

        // Declarar as propriedades
        [PrimaryKey()]
        public Guid Id { get; set; }

        public int IdUsuario { get; set; }

        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
                // Informar a alteração na propriedade
                OnPropertyChanged();
            }
        }

        public string Local
        {
            get
            {
                return local;
            }
            set
            {
                local = value;
                OnPropertyChanged();
            }
        }

        public DateTime DataHoraInicio
        {
            get
            {
                return datahorainicio;
            }
            set
            {
                datahorainicio = value;
                OnPropertyChanged();
            }
        }

        public DateTime DataHoraTermino
        {
            get
            {
                return datahoratermino;
            }
            set
            {
                datahoratermino = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public Guid IdParticipante
        {
            get
            {
                return idparticipante;
            }
            set
            {
                idparticipante = value;
                OnPropertyChanged();
            }
        }

        public string Anotacoes
        {
            get
            {
                return anotacoes;
            }
            set
            {
                anotacoes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

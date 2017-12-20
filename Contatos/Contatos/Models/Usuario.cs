using SQLite;
using System;

namespace Contatos.Models
{
    public class Usuario
    {
        [PrimaryKey]
        //[AutoIncrement]
        //public int Id { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
    }
}

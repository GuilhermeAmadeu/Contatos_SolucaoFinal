using Contatos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.ViewModels
{
    public class PessoaViewModelMem
    {
        public ObservableCollection<Pessoa> Lista { get; set; }

        public PessoaViewModelMem()
        {
            Lista = new ObservableCollection<Pessoa>();
        }

        //public void Salvar(Pessoa item)
        //{
        //    // Se o Id for 0, então é um novo registro
        //    if (item.Id == 0)
        //    {
        //        item.Id = DateTime.Now.Minute + DateTime.Now.Day;
        //        Lista.Add(item);
        //    }
        //    else
        //    {
        //        // Localizar o item existente
        //        var existente = Lista.Where(r => r.Id == item.Id)
        //            .FirstOrDefault();

        //        if (existente != null)
        //        {
        //            // Remover e acrescentar
        //            Lista.Remove(existente);
        //            Lista.Add(item);
                    
        //        }
        //    }
        //}
    }
}

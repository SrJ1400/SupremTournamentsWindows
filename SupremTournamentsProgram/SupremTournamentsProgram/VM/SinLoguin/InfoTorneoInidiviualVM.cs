using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.VM
{
    class InfoTorneoInidiviualVM : ObservableObject
    {
        private TorneoIndividual torneoSelecionado;

        public TorneoIndividual TorneoSelecionado
        {
            get { return torneoSelecionado; }
            set { SetProperty(ref torneoSelecionado, value); }
        }


        public InfoTorneoInidiviualVM()
        {
            TorneoSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestMessage>();

        }



       
    }
}

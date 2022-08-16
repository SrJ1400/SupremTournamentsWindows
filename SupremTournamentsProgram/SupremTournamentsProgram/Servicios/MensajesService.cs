using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SupremTournamentsProgram.Objetos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram
{
    class TorneoIndividualRequestMessage : RequestMessage<TorneoIndividual> { }
    class TorneoIndividualRequestEditarMessage : RequestMessage<TorneoIndividual> { }

    class ListaSolicitudesRequestMessage : RequestMessage<ObservableCollection<Solicitudes>> { }


    class CombateIndividualRequestEditarMessage : RequestMessage<CombatesIndividuales> { }

    class GestorLogueado : RequestMessage<Gestor> { }



    class GestorLogueadoModelo2 : ValueChangedMessage<Gestor>
    {
        public GestorLogueadoModelo2(Gestor gestor) : base(gestor) { }
    }

}

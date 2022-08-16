using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.VM
{
    class InfoCombatesVM : ObservableObject
    {
        private TorneoIndividual torneoSelecionado;

        public TorneoIndividual TorneoSelecionado
        {
            get { return torneoSelecionado; }
            set { SetProperty(ref torneoSelecionado, value); }
        }


        private ObservableCollection<CombatesIndividuales> listaCombates = new ObservableCollection<CombatesIndividuales>();

        public ObservableCollection<CombatesIndividuales> ListaCombates
        {
            get { return listaCombates; }
            set { SetProperty(ref listaCombates, value); }
        }

        //Comandos
        public RelayCommand RefrescarCommand { get; }

        public InfoCombatesVM()
        {
            TorneoSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestMessage>();
            RefrescarCommand = new RelayCommand(RefrescarDatosCombates);

            RefrescarDatosCombates();
        }

        private void RefrescarDatosCombates()
        {
            ListaCombates = ApiService.GetAllCombatesIndividualesFromIdTorneo(TorneoSelecionado.idTorneoIndividual);
        }
    }
}

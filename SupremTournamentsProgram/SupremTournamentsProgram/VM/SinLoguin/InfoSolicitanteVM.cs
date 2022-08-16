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
using System.Windows.Navigation;

namespace SupremTournamentsProgram.VM
{
    class InfoSolicitanteVM : ObservableObject
    {

        private TorneoIndividual torneoSelecionado;

        public TorneoIndividual TorneoSelecionado
        {
            get { return torneoSelecionado; }
            set { SetProperty(ref torneoSelecionado, value); }
        }


        private ObservableCollection<Solicitudes> listaSolicitudes = new ObservableCollection<Solicitudes>();

        public ObservableCollection<Solicitudes> ListaSolicitudes
        {
            get { return listaSolicitudes; }
            set { SetProperty(ref listaSolicitudes, value); }
        }

        private bool puedesSolictar;//Propiedad para bindear si caben mas solicitantes

        public bool PuedesSolictar
        {
            get { return puedesSolictar; }
            set {SetProperty(ref puedesSolictar, value); }
        }


        //Comandos
        public RelayCommand AbrirDialogoSolicitudCommand { get; }
        public RelayCommand RefrescarSolicitudesCommand { get; }
        public InfoSolicitanteVM()
        {

            TorneoSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestMessage>();
            RefrescarSolicitudes();

            AbrirDialogoSolicitudCommand = new RelayCommand(AbrirDialogoSolicitud);
            RefrescarSolicitudesCommand = new RelayCommand(RefrescarSolicitudes);

        }

        private void RefrescarSolicitudes()
        {
            ListaSolicitudes = ApiService.GetAllSolicitudesFromAcceptadasAndIdTorneo(TorneoSelecionado.idTorneoIndividual);

            if (ListaSolicitudes.Count < TorneoSelecionado.solicitudesMaximos && DateTime.Now < DateTimeOffset.FromUnixTimeMilliseconds(TorneoSelecionado.fechaFinalizacion).Date)
            {
                PuedesSolictar = true;
            }
            else
            {
                PuedesSolictar = false;
            }
        }

        private void AbrirDialogoSolicitud()
        {
            NavegacionService.DialogoSolictud();
        }
    }
}

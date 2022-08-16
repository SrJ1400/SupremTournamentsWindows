using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class EditarSolicitudesSolicitadasUCVM : ObservableRecipient
    {
        private TorneoIndividual torneoIndividualSelecionado;

        public TorneoIndividual TorneoIndividualSelecionado
        {
            get { return torneoIndividualSelecionado; }
            set { SetProperty(ref torneoIndividualSelecionado, value); }
        }

        private ObservableCollection<Solicitudes> listaDeSolicitantes;

        public ObservableCollection<Solicitudes> ListaDeSolicitantes
        {
            get { return listaDeSolicitantes; }
            set { SetProperty(ref listaDeSolicitantes, value); }
        }

        private Solicitudes solicitudSelecionada;

        public Solicitudes SolicitudSelecionada
        {
            get { return solicitudSelecionada; }
            set { SetProperty(ref solicitudSelecionada, value); }
        }


        //Comandos
        public RelayCommand AcceptarSolicitudSelecionadaCommand { get; }
        public RelayCommand RechazarSolicitudSelecionadaCommand { get; }
        public RelayCommand RefrescarCommand { get; }



        public EditarSolicitudesSolicitadasUCVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();


            GetAllSolicitudesSolicitadas();

            AcceptarSolicitudSelecionadaCommand = new RelayCommand(AcceptarSolicitudSelecionada);
            RechazarSolicitudSelecionadaCommand = new RelayCommand(RechazarSolicitudSelecionada);
            RefrescarCommand = new RelayCommand(GetAllSolicitudesSolicitadas);


        }

        private void AcceptarSolicitudSelecionada()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                if (SolicitudSelecionada != null)
                {
                    SolicitudSelecionada.estado = Solicitudes.Estado.Acceptada.ToString();
                    ApiService.EditarSolicitudes(SolicitudSelecionada, ApiService.LastElementoSeguridad);
                    GetAllSolicitudesSolicitadas();
                }
                else
                {
                    DialogosService.MensajeError("Error", "Seleccioné el participante a acceptar");
                }
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }

        }
        private void RechazarSolicitudSelecionada()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                if (SolicitudSelecionada != null)
                {
                    SolicitudSelecionada.estado = Solicitudes.Estado.Rechazada.ToString();
                    ApiService.EditarSolicitudes(SolicitudSelecionada, ApiService.LastElementoSeguridad);
                    GetAllSolicitudesSolicitadas();
                }
                else
                {
                    DialogosService.MensajeError("Error", "Seleccioné el participante a rechazar");
                }
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }

        }

        public void GetAllSolicitudesSolicitadas()
        {
            ListaDeSolicitantes = ApiService.GetAllSolicitudesFromSolicitadasAndIdTorneo(TorneoIndividualSelecionado.idTorneoIndividual);
        }

    }
}

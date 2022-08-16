using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class EditarSolicitantesAcceptadosUCVM : ObservableRecipient
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

        private ObservableCollection<string> opcionesEstado = new ObservableCollection<string>();

        public ObservableCollection<string> OpcionesEstado
        {
            get { return opcionesEstado; }
            set { SetProperty(ref opcionesEstado, value); }
        }

        //Comandos
        public RelayCommand EditarSolicitudSelecionadaCommand { get; }
        public RelayCommand GuardarSelecionadaComoContinuaCommand { get; }
        public RelayCommand GetAllSolicitudesAcceptadasCommand { get; }
        public RelayCommand CrearSolicitudCommand { get; }
        public RelayCommand AnadirSolicitanteContinuoCommand { get; }
        public RelayCommand EliminarCommand { get; }


        public EditarSolicitantesAcceptadosUCVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();

            OpcionesEstado.Add(Solicitudes.Estado.Acceptada.ToString());
            OpcionesEstado.Add(Solicitudes.Estado.Rechazada.ToString());
            OpcionesEstado.Add(Solicitudes.Estado.Solicitada.ToString());

            GetAllSolicitudesAcceptadas();

            EditarSolicitudSelecionadaCommand = new RelayCommand(EditarSolicitudSelecionada);
            GuardarSelecionadaComoContinuaCommand = new RelayCommand(GuardarSelecionadaComoContinua);
            GetAllSolicitudesAcceptadasCommand = new RelayCommand(GetAllSolicitudesAcceptadas);
            CrearSolicitudCommand = new RelayCommand(CrearSolicitud);
            AnadirSolicitanteContinuoCommand = new RelayCommand(AnadirSolicitanteContinuo);
            EliminarCommand = new RelayCommand(EliminarSolicitante);

        }

        private void EliminarSolicitante()
        {

            if (SolicitudSelecionada != null)
            {
                if (DialogosService.MensajeInformacionDosOpciones("Confiramación", "¿Seguro que desea eliminar el participante " + SolicitudSelecionada.nombreSolicitudes + "?"))
                {
                    if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                    {
                        ApiService.DeleteSolicitudes(SolicitudSelecionada.idSolicitudes, ApiService.LastElementoSeguridad);
                        GetAllSolicitudesAcceptadas();
                    }
                    else
                    {
                        DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                    }
                }
            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccioné el participante a eliminar");
            }



        }

        private void AnadirSolicitanteContinuo()
        {
            bool opcionUsuario = true;
            if (ListaDeSolicitantes.Count >= TorneoIndividualSelecionado.solicitudesMaximos)
            {
                opcionUsuario = DialogosService.MensajeInformacionDosOpciones("Confiramación", "¿Seguro que desea crear un partipante ya has llegado ha " + TorneoIndividualSelecionado.solicitudesMaximos + "?");

            }

            if (opcionUsuario)
            {
                if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                {
                    NavegacionService.AndirSolicitudContinua();
                }
                else
                {
                    DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                }
            }
        }

        private void CrearSolicitud()
        {
            bool opcionUsuario = true;
            if (ListaDeSolicitantes.Count >= TorneoIndividualSelecionado.solicitudesMaximos)
            {
                opcionUsuario = DialogosService.MensajeInformacionDosOpciones("Confiramación", "¿Seguro que desea crear un partipante ya has llegado ha " + TorneoIndividualSelecionado.solicitudesMaximos + "?");
                
            }

            if (opcionUsuario)
            {
                NavegacionService.CrearSolicitudAcceptada();
            }
        }

        private void GuardarSelecionadaComoContinua()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                if (SolicitudSelecionada != null)
                {
                    SolicitudesContinuas solicitudesContinuas = new SolicitudesContinuas(SolicitudSelecionada, 0);
                    ApiService.CreateSolicitudesContinuas(solicitudesContinuas, ApiService.LastElementoSeguridad);
                }
                else
                {
                    DialogosService.MensajeError("Error", "Seleccioné el participante a guardar como participante continuo");
                }
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

        private void EditarSolicitudSelecionada()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                if (SolicitudSelecionada != null)
                {
                    ApiService.EditarSolicitudes(SolicitudSelecionada, ApiService.LastElementoSeguridad);
                    GetAllSolicitudesAcceptadas();
                }
                else
                {
                    DialogosService.MensajeError("Error", "Seleccioné el participante a cambiar");
                }
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

        public void GetAllSolicitudesAcceptadas()
        {
            ListaDeSolicitantes = ApiService.GetAllSolicitudesFromAcceptadasAndIdTorneo(TorneoIndividualSelecionado.idTorneoIndividual);
        }


        public void AbrirDialogoImagen()
        {
            if (SolicitudSelecionada != null)
            {
                try
                {
                    SolicitudSelecionada.fotoUrlSolicitudes = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                }
                catch (Exception)
                {
                    DialogosService.MensajeError("Error", "Error con la imagen");
                }
            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccioné el participante a eliminar");
            }
        }

    }
}

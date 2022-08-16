using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class DialogoAnadirSolicitudContinuaVM : ObservableObject
    {
        private ObservableCollection<SolicitudesContinuas> solicitudesContinuas;

        public ObservableCollection<SolicitudesContinuas> SolicitudesContinuas
        {
            get { return solicitudesContinuas; }
            set { SetProperty(ref solicitudesContinuas, value); }
        }

        private SolicitudesContinuas solicitudContinuaSelecionada;

        public SolicitudesContinuas SolicitudContinuaSelecionada
        {
            get { return solicitudContinuaSelecionada; }
            set { SetProperty(ref solicitudContinuaSelecionada, value); }
        }

        private TorneoIndividual torneoIndividualSelecionado;

        public TorneoIndividual TorneoIndividualSelecionado
        {
            get { return torneoIndividualSelecionado; }
            set { SetProperty(ref torneoIndividualSelecionado, value); }
        }

        public DialogoAnadirSolicitudContinuaVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();

            SolicitudesContinuas = ApiService.GetAllSolicitudesContinuas(ApiService.LastElementoSeguridad);
        }

        public void GuardarComoSolicitudAcceptada()
        {

            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                if (SolicitudContinuaSelecionada != null)
                {


                    Solicitudes solicitud = new Solicitudes(SolicitudContinuaSelecionada, 0, Solicitudes.Estado.Acceptada.ToString(), TorneoIndividualSelecionado);


                    DateTime thisDay = DateTime.Today;

                    DateTime birthDay = DateTimeOffset.FromUnixTimeMilliseconds(solicitud.fechaNacimiento).Date;

                    TimeSpan anyosDiferencia = thisDay - birthDay;
                    int edadCalculada = ((int)Math.Round(anyosDiferencia.TotalDays / 365.25));


                    bool edadValidad = false;

                    if (!TorneoIndividualSelecionado.menoresEdad && (edadCalculada > 17))
                    {
                        edadValidad = true;
                    }
                    if (TorneoIndividualSelecionado.menoresEdad && (edadCalculada > 0))
                    {
                        edadValidad = true;
                    }

                    if (edadValidad)
                    {
                        ApiService.CreateSolicitudes(solicitud);
                    }
                    else
                    {
                        DialogosService.MensajeError("Edad no permitida", "Los menores de edad no estan permitidos en este torneo");
                    }
                }
                else
                {
                    DialogosService.MensajeError("Error", "No se seleciono ninguna solicitud continua");
                }
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }

        }
    }
}

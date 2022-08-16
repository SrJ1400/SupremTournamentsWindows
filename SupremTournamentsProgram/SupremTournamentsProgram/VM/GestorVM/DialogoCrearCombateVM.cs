using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class DialogoCrearCombateVM : ObservableRecipient
    {
        private ObservableCollection<Solicitudes> listaSolicitudes;

        public ObservableCollection<Solicitudes> ListaSolicitudes
        {
            get { return listaSolicitudes; }
            set { SetProperty(ref listaSolicitudes, value); }
        }

        private TorneoIndividual torneoIndividualSelecionado;

        public TorneoIndividual TorneoIndividualSelecionado
        {
            get { return torneoIndividualSelecionado; }
            set { SetProperty(ref torneoIndividualSelecionado, value); }
        }

        private CombatesIndividuales combatesIndividualesACrear;

        public CombatesIndividuales CombatesIndividualesACrear
        {
            get { return combatesIndividualesACrear; }
            set { SetProperty(ref combatesIndividualesACrear, value); }
        }

        public DialogoCrearCombateVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();
            

            CombatesIndividualesACrear = new CombatesIndividuales(0, null, null, null, TorneoIndividualSelecionado);

            GetAllSolicitudesAcceptadas();

        }

        public void GetAllSolicitudesAcceptadas()
        {
            ListaSolicitudes = ApiService.GetAllSolicitudesFromAcceptadasAndIdTorneo(TorneoIndividualSelecionado.idTorneoIndividual);
        }
        public void CrearCombate()
        {
            if (CombatesIndividualesACrear.idSolicitud1 != null && CombatesIndividualesACrear.idSolicitud2 != null)
            {
                if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                {
                    ApiService.CreateCombatesindividuales(CombatesIndividualesACrear, ApiService.LastElementoSeguridad);
                }
                else
                {
                    DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                }
            }
            else
            {
                DialogosService.MensajeError("Combate invalido", "Es necesario elegir contrincantes");
            }

        }

    }
}

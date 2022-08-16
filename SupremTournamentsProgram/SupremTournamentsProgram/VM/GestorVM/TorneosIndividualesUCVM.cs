using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class TorneosIndividualesUCVM : ObservableObject
    {
        private ObservableCollection<TorneoIndividual> listaTorneosIndividuales;

        public ObservableCollection<TorneoIndividual> ListaTorneosIndividuales
        {
            get { return listaTorneosIndividuales; }
            set { SetProperty(ref listaTorneosIndividuales, value); }
        }

        private TorneoIndividual torneosIndividualesSelecionado;

        public TorneoIndividual TorneosIndividualesSelecionado
        {
            get { return torneosIndividualesSelecionado; }
            set { SetProperty(ref torneosIndividualesSelecionado, value); }
        }


        private Gestor gestorLogueado;

        public Gestor GestorLogueado
        {
            get { return gestorLogueado; }
            set { SetProperty(ref gestorLogueado, value); }
        }


        //Comandos
        public RelayCommand AbrirDialogoCrearTorneoIndiviualCommand { get; }
        public RelayCommand RefrescarCommand { get; }
        public RelayCommand EliminarCommand { get; }

        public TorneosIndividualesUCVM()
        {
            GestorLogueado = new Gestor(0, "", "", "");

            WeakReferenceMessenger.Default.Register<GestorLogueadoModelo2>(this, (r, m) =>
            {
                GestorLogueado = m.Value;
                GetAllTorneosIndividualesByEmail();
            });

            GetAllTorneosIndividualesByEmail();
            AbrirDialogoCrearTorneoIndiviualCommand = new RelayCommand(AbrirDialogoCrearTorneoIndiviual);
            RefrescarCommand = new RelayCommand(GetAllTorneosIndividualesByEmail);
            EliminarCommand = new RelayCommand(EliminarTorneo);
            //Mensajeria

            WeakReferenceMessenger.Default.Register<TorneosIndividualesUCVM, TorneoIndividualRequestEditarMessage>(this, (r, m) =>
            {
                m.Reply(r.TorneosIndividualesSelecionado);//Proporciono la informacion (cuando lliegan mensages)
            });
        }

        private void EliminarTorneo()
        {

            if (TorneosIndividualesSelecionado != null)
            {
                if (DialogosService.MensajeInformacionDosOpciones("Confiramación", "¿Seguro que desea eliminar el Torneo " + TorneosIndividualesSelecionado.nombreTorneoIndividual + "?"))
                {
                    if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                    {
                        ApiService.DeleteTorneosId(TorneosIndividualesSelecionado.idTorneoIndividual, ApiService.LastElementoSeguridad);
                        GetAllTorneosIndividualesByEmail();
                    }
                    else
                    {
                        DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                    }
                }
            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccioné el Torneo a eliminar");
            }



        }

        private void AbrirDialogoCrearTorneoIndiviual()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                NavegacionService.CrearTorneoIndividual();
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

        public void GetAllTorneosIndividualesByEmail()
        {
            ListaTorneosIndividuales = ApiService.GetTorneosEmailGestor(GestorLogueado.email);
        }

    }
}

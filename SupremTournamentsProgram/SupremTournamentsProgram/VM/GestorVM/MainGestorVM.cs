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
using System.Windows.Controls;

namespace SupremTournamentsProgram.VM
{
    class MainGestorVM: ObservableRecipient
    {
        private Gestor gestorLogueado;

        public Gestor GestorLogueado
        {
            get { return gestorLogueado; }
            set {SetProperty(ref gestorLogueado, value); }
        }


        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }


        //Comandos
        public RelayCommand AbrirSolicitudesContinuasCommand { get; }
        public RelayCommand AbrirTorneosIndividualesCommand { get; }


        public MainGestorVM()
        {

            GestorLogueado = WeakReferenceMessenger.Default.Send<GestorLogueado>();

            AbrirSolicitudesContinuasCommand = new RelayCommand(AbrirSolicitudesContinuas);
            AbrirTorneosIndividualesCommand = new RelayCommand(AbrirTorneosIndividuales);
            AbrirTorneosIndividuales();
        }

        private void AbrirTorneosIndividuales()
        {
            ContenidoVentana = NavegacionService.TorneosIndividualesUC();
        }

        private void AbrirSolicitudesContinuas()
        {
            ContenidoVentana = NavegacionService.SolicitudesContinuasUC();
        }
    }
}

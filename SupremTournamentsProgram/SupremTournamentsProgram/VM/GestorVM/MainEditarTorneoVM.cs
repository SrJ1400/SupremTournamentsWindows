using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class MainEditarTorneoVM: ObservableRecipient
    {

        private TorneoIndividual torneoSelecionado;

        public TorneoIndividual TorneoSelecionado
        {
            get { return torneoSelecionado; }
            set {SetProperty(ref torneoSelecionado, value);
                if (value != null) IsSelected = true;
                else IsSelected = false;
            }
        }

        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set {SetProperty(ref isSelected, value); }
        }



        //Comandos
        public RelayCommand AbrirEditarDatosTorneoIndividualCommand { get; }
        public RelayCommand AbrirEditarDatosSolicitudesAcceptadasCommand { get; }
        public RelayCommand AbrirEditarDatosSolicitudesSolicitadasCommand { get; }
        public RelayCommand AbrirEditarDatosCombatesCommand { get; }
        public MainEditarTorneoVM()
        {
            AbrirEditarDatosTorneoIndividualCommand = new RelayCommand(AbrirEditarInfoTorneoIndividual);
            AbrirEditarDatosSolicitudesAcceptadasCommand = new RelayCommand(AbrirEditarDatosSolicitudesAcceptadas);
            AbrirEditarDatosSolicitudesSolicitadasCommand = new RelayCommand(AbrirEditarDatosSolicitudesSolicitadas);
            AbrirEditarDatosCombatesCommand = new RelayCommand(AbrirEditarDatosCombates);
            TorneoSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();
            if (TorneoSelecionado != null)
            {
                AbrirEditarInfoTorneoIndividual();
            }
            else
            {
                ContenidoVentana = NavegacionService.ErrorSeleccionarTorneo();
            }

        }

        private void AbrirEditarDatosCombates()
        {
            ContenidoVentana = NavegacionService.DatosCombates();
        }
        private void AbrirEditarDatosSolicitudesSolicitadas()
        {
            ContenidoVentana = NavegacionService.EditarDatosSolicitudesSolicitadas();
        }
        private void AbrirEditarDatosSolicitudesAcceptadas()
        {
            ContenidoVentana = NavegacionService.EditarDatosSolicitudesAcceptadas();
        }

        private void AbrirEditarInfoTorneoIndividual()
        {
            ContenidoVentana = NavegacionService.EditarDatosTorneoIndividual();
        }

        
    }
}

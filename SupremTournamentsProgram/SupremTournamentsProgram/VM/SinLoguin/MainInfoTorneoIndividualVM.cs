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
namespace SupremTournamentsProgram.VM
{
    class MainInfoTorneoIndividualVM : ObservableObject
    {
        private TorneoIndividual torneoIndividual;

        public TorneoIndividual TorneoIndividual
        {
            get { return torneoIndividual; }
            set {SetProperty(ref torneoIndividual, value); }
        }

        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }



        //Comandos de navegacion
        public RelayCommand AbrirInfoTorneoCommand { get; }
        public RelayCommand AbrirInfoSolicitantesCommand { get; }
        public RelayCommand AbrirInfoCombatesCommand { get; }


        public MainInfoTorneoIndividualVM()
        {
            TorneoIndividual = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestMessage>();
            AbrirInfoTorneo();
            AbrirInfoTorneoCommand = new RelayCommand(AbrirInfoTorneo);
            AbrirInfoSolicitantesCommand = new RelayCommand(AbrirInfoSolicitantes);
            AbrirInfoCombatesCommand = new RelayCommand(AbrirInfoCombates);

        }


        //Metodos de Navegacion
        private void AbrirInfoTorneo()
        {
            ContenidoVentana = NavegacionService.InfoTorneoUserControl();
        }  
        private void AbrirInfoSolicitantes()
        {
            ContenidoVentana = NavegacionService.InfoSolicitantesUseControl();
        }     
        
        private void AbrirInfoCombates()
        {
            ContenidoVentana = NavegacionService.InfoCombatesUseControl();
        }

    }
}

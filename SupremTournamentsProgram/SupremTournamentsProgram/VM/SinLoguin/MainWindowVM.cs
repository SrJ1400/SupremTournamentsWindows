using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Servicios;
using System.Windows;
using System.Windows.Controls;

namespace SupremTournamentsProgram.VM
{
    class MainWindowVM : ObservableObject
    {


        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }

        private bool isLoged;

        public bool IsLoged
        {
            get { return isLoged; }
            set { SetProperty(ref isLoged, value); }
        }


        private int tabSelecionado;

        public int TabSelecionado
        {
            get { return tabSelecionado; }
            set
            {
                SetProperty(ref tabSelecionado, value);
                switch ((int)value)
                {
                    case 1:
                        ContenidoVentana = NavegacionService.VerGeneralTorneo();
                        break;

                    case 2:
                        ContenidoVentana = NavegacionService.VerTorneoIndividual();
                        break;

                    case 3:
                        ContenidoVentana = NavegacionService.Loguearse();
                        break;
                    case 4:
                        ContenidoVentana = NavegacionService.Registrarse();
                        break;

                    case 5:
                        ContenidoVentana = NavegacionService.MainGestor();
                        break;

                    case 6:
                        ContenidoVentana = NavegacionService.MainEditarTorneoIndividual();
                        break;

                    case 7:
                        System.Diagnostics.Process.Start(@".\DocumentacionSupremTournaments.chm");
                        break;

                    case 8:
                        IsLoged = false;
                        TabSelecionado = 1;
                        break;


                    default:
                        ContenidoVentana = NavegacionService.VerDefaultUC();
                        break;
                }

            }
        }

        private WindowState estadoPantalla;

        public WindowState EstadoPantalla
        {
            get { return estadoPantalla; }
            set { SetProperty(ref estadoPantalla, value); }
        }

        /*public bool PantallaTrasparente { get; set; }
        public string EstiloPantalla { get; set; }*/



        public RelayCommand PantallaCompletaCommand { get; }
        public RelayCommand PantallaNormalCommand { get; }


        public MainWindowVM()
        {
            TabSelecionado = 1;
            EstadoPantalla = WindowState.Normal;
            //PantallaTrasparente = false;
            //EstiloPantalla = "SingleBorderWindow";


            PantallaCompletaCommand = new RelayCommand(PantallaCompleta);
            PantallaNormalCommand = new RelayCommand(PantallaNormal);


            //Control de registro
            WeakReferenceMessenger.Default.Register<GestorLogueadoModelo2>(this, (r, m) =>
            {
                if (m.Value.email.Equals("")) IsLoged = false;
                else IsLoged = true;
            });

        }

        public void PantallaCompleta()
        {
            //PantallaTrasparente = true;
            //EstiloPantalla = "none";
            EstadoPantalla = WindowState.Maximized;
        }

        public void PantallaNormal()
        {
            //EstiloPantalla = "SingleBorderWindow";
            //PantallaTrasparente = false;
            EstadoPantalla = WindowState.Normal;
        }


    }
}

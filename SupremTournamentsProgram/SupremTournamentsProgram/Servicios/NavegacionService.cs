using SupremTournamentsProgram.Vistas.Gestor;
using SupremTournamentsProgram.Vistas.SinLogin;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SupremTournamentsProgram.Servicios
{
    static class NavegacionService
    {
        static readonly GeneralTorneo generalTorneo = new GeneralTorneo();
        static readonly Login login = new Login();

        static public UserControl VerDefaultUC()
        {
            DefaultUC defaultUC = new DefaultUC();
            return defaultUC;
        }


        static public UserControl VerGeneralTorneo()
        {
            return generalTorneo;
        }

        static public UserControl VerTorneoIndividual()
        {
            MainInfoTorneoIndividual mainInfoTorneoIndividual = new MainInfoTorneoIndividual();
            return mainInfoTorneoIndividual;
        }

        static public void VerHomePage()
        {
            MainWindow nueva = new MainWindow();
            nueva.Show();
        }

        static public UserControl InfoTorneoUserControl()
        {
            InfoTorneoIndividual infoTorneoIndividual = new InfoTorneoIndividual();
            return infoTorneoIndividual;
        }

        static public UserControl InfoSolicitantesUseControl()
        {
            InfoSolicitantes infoSolicitudes = new InfoSolicitantes();
            return infoSolicitudes;
        }

        static public void DialogoSolictud()
        {
            FormularioSolicitanteASolicitar nueva = new FormularioSolicitanteASolicitar();
            nueva.ShowDialog();
        }

        static public UserControl InfoCombatesUseControl()
        {
            InfoCombates infoCombates = new InfoCombates();
            return infoCombates;
        }

        static public UserControl Registrarse()
        {
            Registrarse registrarse = new Registrarse();
            return registrarse;
        }

        static public UserControl Loguearse()
        {
            return login;
        }


        //Gestor
        static public UserControl MainGestor()
        {
            MainGestor mainGestor = new MainGestor();
            return mainGestor;
        }

        static public UserControl SolicitudesContinuasUC()
        {
            SolicitudesContinuasUC solicitudesContinuas = new SolicitudesContinuasUC();
            return solicitudesContinuas;
        }

        static public void CrearSolicitudContinua()
        {
            CrearSolicitudContinua crearSolicitudContinua = new CrearSolicitudContinua();
            crearSolicitudContinua.ShowDialog();
        }


        static TorneosIndividualesUC tornenosIndividualesUC = new TorneosIndividualesUC();

        static public UserControl TorneosIndividualesUC()
        {
            return tornenosIndividualesUC;
        }

        static public void CrearTorneoIndividual()
        {
            DialogoCrearTorneoInidvidual dialogoCrearTorneoIndividual = new DialogoCrearTorneoInidvidual();
            dialogoCrearTorneoIndividual.ShowDialog();
        }

        static public UserControl MainEditarTorneoIndividual()
        {
            MainEditarTornoIndividual editarDatosTorneoInidvidual = new MainEditarTornoIndividual();
            return editarDatosTorneoInidvidual;
        }

        static public UserControl EditarDatosTorneoIndividual()
        {
            EditarDatosTorneoInidvidual editarDatosTorneoInidvidual = new EditarDatosTorneoInidvidual();
            return editarDatosTorneoInidvidual;
        }

        static public UserControl EditarDatosSolicitudesAcceptadas()
        {
            EditarSolicitantesAcceptadosUC editarSolicitantesAcceptadosUC = new EditarSolicitantesAcceptadosUC();
            return editarSolicitantesAcceptadosUC;
        }

        static public void CrearSolicitudAcceptada()
        {
            CrearSolicitudAcceptada crearSolicitudAcceptada = new CrearSolicitudAcceptada();
            crearSolicitudAcceptada.ShowDialog();
        }

        static public void AndirSolicitudContinua()
        {
            AnadirSolicitudContinua anadirSolicitudContinua = new AnadirSolicitudContinua();
            anadirSolicitudContinua.ShowDialog();
        }


        static public UserControl EditarDatosSolicitudesSolicitadas()
        {
            EditarSolicitudesSolicitadasUC editarSolicitudesSolicitadasUC = new EditarSolicitudesSolicitadasUC();
            return editarSolicitudesSolicitadasUC;
        }


        static public UserControl DatosCombates()
        {
            EditarCombatesUC editarCombatesUC = new EditarCombatesUC();
            return editarCombatesUC;
        }


        static public void CrearConbatesDialogo()
        {
            DialogoCrearCombate dialogoCrearCombate = new DialogoCrearCombate();
            dialogoCrearCombate.ShowDialog();
        }


        static public UserControl ErrorSeleccionarTorneo()
        {
            TorneoNoSelecionadoErrorUC editarCombatesUC = new TorneoNoSelecionadoErrorUC();
            return editarCombatesUC;
        }

    }
}

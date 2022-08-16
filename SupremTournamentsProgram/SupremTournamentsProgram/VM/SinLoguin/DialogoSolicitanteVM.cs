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

namespace SupremTournamentsProgram.VM
{


    class DialogoSolicitanteVM : ObservableRecipient
    {
        private Solicitudes solicitud;

        public Solicitudes Solicitud
        {
            get { return solicitud; }
            set { SetProperty(ref solicitud, value); }
        }

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        private TorneoIndividual torneoIndividualSelecionado;

        public TorneoIndividual TorneoIndividualSelecionado
        {
            get { return torneoIndividualSelecionado; }
            set { SetProperty(ref torneoIndividualSelecionado, value); }
        }


        private ObservableCollection<Solicitudes> listaSolicitudes = new ObservableCollection<Solicitudes>();

        public ObservableCollection<Solicitudes> ListaSolicitudes
        {
            get { return listaSolicitudes; }
            set { SetProperty(ref listaSolicitudes, value); }
        }


        public DialogoSolicitanteVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestMessage>();

            DateTimeOffset datetime = DateTime.Now;
            Solicitud = new Solicitudes(0, "", "https://dintactividadjuego.blob.core.windows.net/imagenes/pesona.png", datetime.ToUnixTimeMilliseconds(), "", "", Solicitudes.Estado.Solicitada.ToString(), TorneoIndividualSelecionado);
            Imagen = "https://dintactividadjuego.blob.core.windows.net/imagenes/pesona.png";
        }

        public void EnviarSolicitud()
        {

            DateTimeOffset datetime = DateTime.Now;

            DateTime thisDay = DateTime.Today;

            DateTime birthDay = DateTimeOffset.FromUnixTimeMilliseconds(Solicitud.fechaNacimiento).Date;

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


                ApiService.CreateSolicitudes(Solicitud);
            }
            else
            {
                DialogosService.MensajeError("Edad no permitida", "Los menores de edad no estan permitidos en este torneo");
            }


        }

        public void AbrirDialogoImagen()
        {
            try
            {
                Solicitud.fotoUrlSolicitudes = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                Imagen = Solicitud.fotoUrlSolicitudes;
            }
            catch (Exception)
            {
                DialogosService.MensajeError("Error", "Error con la imagen");
            }

        }

    }
}

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class DialogoCrearSolicitudAcceptadaVM: ObservableRecipient
    {
        private TorneoIndividual torneoIndividualSelecionado;

        public TorneoIndividual TorneoIndividualSelecionado
        {
            get { return torneoIndividualSelecionado; }
            set {SetProperty(ref torneoIndividualSelecionado, value); }
        }

        private Solicitudes solicitudACrear;

        public Solicitudes SolicitudACrear
        {
            get { return solicitudACrear; }
            set {SetProperty(ref solicitudACrear, value); }
        }

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set {SetProperty(ref imagen, value); }
        }


        public DialogoCrearSolicitudAcceptadaVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();

            DateTimeOffset datetime = DateTime.Now;
            SolicitudACrear = new Solicitudes(0, "", "https://dintactividadjuego.blob.core.windows.net/imagenes/pesona.png", datetime.ToUnixTimeMilliseconds(), "", "",Solicitudes.Estado.Acceptada.ToString(), TorneoIndividualSelecionado);
            Imagen = "https://dintactividadjuego.blob.core.windows.net/imagenes/pesona.png";
        }

        public void CrearSolicitud()
        {
            DateTimeOffset datetime = DateTime.Now;

            DateTime thisDay = DateTime.Today;

            DateTime birthDay = DateTimeOffset.FromUnixTimeMilliseconds(SolicitudACrear.fechaNacimiento).Date;

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
                ApiService.CreateSolicitudes(SolicitudACrear);
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
                SolicitudACrear.fotoUrlSolicitudes = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                Imagen = SolicitudACrear.fotoUrlSolicitudes;
            }
            catch (Exception)
            {
                DialogosService.MensajeError("Error", "Error con la imagen");
            }

        }

    }
}

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class DialogoCrearTorneoIndividualVM : ObservableObject
    {

        private TorneoIndividual torneoIndividualACrear;

        public TorneoIndividual TorneoIndividualACrear
        {
            get { return torneoIndividualACrear; }
            set { SetProperty(ref torneoIndividualACrear, value); }
        }

        private Gestor gestorLogueado;

        public Gestor GestorLogueado
        {
            get { return gestorLogueado; }
            set { SetProperty(ref gestorLogueado, value); }
        }


        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        public DialogoCrearTorneoIndividualVM()
        {
            DateTimeOffset datetime = DateTime.Now;
            GestorLogueado = WeakReferenceMessenger.Default.Send<GestorLogueado>();
            TorneoIndividualACrear = new TorneoIndividual(0, "", "https://dintactividadjuego.blob.core.windows.net/imagenes/favicon.ico", "", "", 3, 0, datetime.ToUnixTimeMilliseconds(), datetime.ToUnixTimeMilliseconds(), false, GestorLogueado);
            Imagen = "https://dintactividadjuego.blob.core.windows.net/imagenes/favicon.ico";
        }


        public void CrearTorneoIndividual()
        {
            if (TorneoIndividualACrear.fechaFinalizacion > TorneoIndividualACrear.fechaInicio)
            {

                if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                {
                    ApiService.CreateTorneo(TorneoIndividualACrear, ApiService.LastElementoSeguridad);
                }
                else
                {
                    DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                }
            }
            else
            {
                DialogosService.MensajeError("Fechas erroneas", "La fecha de finalización debe de ser mayor a la fecha inicial");
            }
        }

        public void AbrirDialogoImagen()
        {
            try
            {
                TorneoIndividualACrear.fotoUrlTorneoIndividual = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                Imagen = TorneoIndividualACrear.fotoUrlTorneoIndividual;
            }
            catch (Exception)
            {
                DialogosService.MensajeError("Error", "Error con la imagen");
            }

        }

    }
}

using Microsoft.Toolkit.Mvvm.ComponentModel;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class DialogoCrearSolitudContinuaVM : ObservableObject
    {
        private SolicitudesContinuas solicituContinuaACrear;

        public SolicitudesContinuas SolicitudContinuaACrear
        {
            get { return solicituContinuaACrear; }
            set { SetProperty(ref solicituContinuaACrear, value); }
        }

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }



        public DialogoCrearSolitudContinuaVM()
        {
            DateTimeOffset datetime = DateTime.Now;
            SolicitudContinuaACrear = new SolicitudesContinuas(0, "", "https://dintactividadjuego.blob.core.windows.net/imagenes/pesona.png", datetime.ToUnixTimeMilliseconds(), "", "");
            Imagen = "https://dintactividadjuego.blob.core.windows.net/imagenes/pesona.png";
        }

        public void CrearSolicitudContia()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                ApiService.CreateSolicitudesContinuas(SolicitudContinuaACrear, ApiService.LastElementoSeguridad);
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

        public void AbrirDialogoImagen()
        {
            try
            {
                SolicitudContinuaACrear.fotoUrlSolicitudesContinuas = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                Imagen = SolicitudContinuaACrear.fotoUrlSolicitudesContinuas;
            }
            catch (Exception)
            {
                DialogosService.MensajeError("Error", "Error de imagen");
            }

        }
    }
}

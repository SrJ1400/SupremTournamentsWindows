using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class SolicitudesContinuasVM : ObservableObject
    {
        private ObservableCollection<SolicitudesContinuas> listaSolicitudeContinuas;

        public ObservableCollection<SolicitudesContinuas> ListaSolicitudeContinuas
        {
            get { return listaSolicitudeContinuas; }
            set { SetProperty(ref listaSolicitudeContinuas, value); }
        }

        private SolicitudesContinuas solicitudContinuaSelecionada;

        public SolicitudesContinuas SolicitudContinuaSelecionada
        {
            get { return solicitudContinuaSelecionada; }
            set { SetProperty(ref solicitudContinuaSelecionada, value); }
        }

        public void AbrirDialogoImagen()
        {
            if (SolicitudContinuaSelecionada != null)
            {
                try
                {
                    SolicitudContinuaSelecionada.fotoUrlSolicitudesContinuas = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                }
                catch (Exception)
                {
                    DialogosService.MensajeError("Error", "Error con la imagen");
                }
            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccione antes la linea del participante continuo a modificar");
            }
        }



        //Comandos
        public RelayCommand CrearSolicitudContinuaCommand { get; }
        public RelayCommand RefrescarCommand { get; }
        public RelayCommand EliminarCommand { get; }
        public RelayCommand EditarCommand { get; }

        public SolicitudesContinuasVM()
        {
            //CrearSolicitudContinua
            GetAllSolicitudesContinuas();
            CrearSolicitudContinuaCommand = new RelayCommand(CrearSolicitudContinua);
            RefrescarCommand = new RelayCommand(GetAllSolicitudesContinuas);
            EliminarCommand = new RelayCommand(ElimiarSolicitudContinua);
            EditarCommand = new RelayCommand(EditarSolicitudContinua);
        }

        private void ElimiarSolicitudContinua()
        {

            if (SolicitudContinuaSelecionada != null)
            {
                if (DialogosService.MensajeInformacionDosOpciones("Confiramación", "¿Seguro que desea eliminar el participante continuo " + SolicitudContinuaSelecionada.nombreSolicitudesContinuas + "?"))
                {
                    if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                    {
                        ApiService.DeleteSolicitudesContinuas(SolicitudContinuaSelecionada.idSolicitudesContinuas, ApiService.LastElementoSeguridad);
                        GetAllSolicitudesContinuas();
                    }
                    else
                    {
                        DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                    }
                }
            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccione antes la linea del participante continuo a borrar");
            }            
        }

        private void EditarSolicitudContinua()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                if (SolicitudContinuaSelecionada != null)
                {
                    ApiService.EditarSolicitudesContinuas(SolicitudContinuaSelecionada, ApiService.LastElementoSeguridad);
                    GetAllSolicitudesContinuas();
                }
                else
                {
                    DialogosService.MensajeError("Error", "Seleccione antes la linea del participante continuo a guardar cambios");
                }
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }


        }

        private void GetAllSolicitudesContinuas()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                ListaSolicitudeContinuas = ApiService.GetAllSolicitudesContinuas(ApiService.LastElementoSeguridad);
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

        private void CrearSolicitudContinua()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                NavegacionService.CrearSolicitudContinua();
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

    }
}

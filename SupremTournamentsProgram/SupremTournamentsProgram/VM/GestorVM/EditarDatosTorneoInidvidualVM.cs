using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class EditarDatosTorneoInidvidualVM : ObservableRecipient
    {
        private TorneoIndividual torneoIndividualAEditar;

        public TorneoIndividual TorneoIndividualAEditar
        {
            get { return torneoIndividualAEditar; }
            set { SetProperty(ref torneoIndividualAEditar, value); }
        }

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        //Comandos
        public RelayCommand EditarTorneoCommand { get; }

        public EditarDatosTorneoInidvidualVM()
        {
            TorneoIndividualAEditar = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();
            Imagen = TorneoIndividualAEditar.fotoUrlTorneoIndividual;

            EditarTorneoCommand = new RelayCommand(EditarTorneo);
        }

        private void EditarTorneo()
        {
            if (TorneoIndividualAEditar.fechaFinalizacion > TorneoIndividualAEditar.fechaInicio)
            {
                if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                {
                    ApiService.UpdateTorneo(TorneoIndividualAEditar, ApiService.LastElementoSeguridad);
                }
                else
                {
                    DialogosService.MensajeError("Sesion caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
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
                TorneoIndividualAEditar.fotoUrlTorneoIndividual = AzureService.GuardarImagen(DialogosService.AbrirArchivoDialogo("JPG (*.jpg)|*.jpg|PNG (*.png*)|*.png*"));
                Imagen = TorneoIndividualAEditar.fotoUrlTorneoIndividual;
            }
            catch (Exception)
            {
                DialogosService.MensajeError("Error", "Error de imagen");
            }

        }

    }
}

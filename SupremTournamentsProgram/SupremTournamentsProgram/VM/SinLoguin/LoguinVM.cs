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
using System.Windows.Threading;

namespace SupremTournamentsProgram.VM.SinLoguin
{
    class LoguinVM: ObservableObject
    {

        private string email;

        public string Email
        {
            get { return email; }
            set {SetProperty(ref email, value); }
        }

        private string contrasenya;

        public string Contrasenya
        {
            get { return contrasenya; }
            set {SetProperty(ref contrasenya, value); }
        }

        private ElementoSeguridad elementoSeguridadAPI;

        public ElementoSeguridad ElementoSeguridadAPI
        {
            get { return elementoSeguridadAPI; }
            set {SetProperty(ref elementoSeguridadAPI, value); }
        }

        private Gestor gestorRecibidoAPI;

        public Gestor GestorRecibidoAPI
        {
            get { return gestorRecibidoAPI; }
            set {SetProperty(ref gestorRecibidoAPI, value); }
        }


        //Comandos
        public RelayCommand LoguinCommand { get; }

        public LoguinVM()
        {
            LoguinCommand = new RelayCommand(Loguearse);

            //Mando el Gestor
            WeakReferenceMessenger.Default.Register<LoguinVM, GestorLogueado>(this, (r, m) =>
            {
                m.Reply(r.GestorRecibidoAPI);
            });
        }

        private void Loguearse()
        {
            //Faltan validaciones
            try
            {
                ElementoSeguridadAPI = ApiService.GetElementoSeguridad(Email, Gestor.CifrarSha256(Contrasenya));
                CheckJWT checkJWT = ApiService.CheckApiKey(ElementoSeguridadAPI);
                if (checkJWT.validate)
                {
                    GestorRecibidoAPI = ApiService.GetGestorByEmail(Email, ElementoSeguridadAPI);
                    WeakReferenceMessenger.Default.Send(new GestorLogueadoModelo2(GestorRecibidoAPI));
                    Email = "";
                    Contrasenya = "";                    
                }
                else
                {
                    DialogosService.MensajeError("Error", "Usuario o contraseña invalidos");
                }
            }
            catch (Exception)
            {
                DialogosService.MensajeError("Error", "Usuario o contraseña invalidos");
            }

        }
    }
}
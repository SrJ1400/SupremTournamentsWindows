using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SupremTournamentsProgram.VM
{
    class RegistrarseVM : ObservableObject
    {
        private Gestor gestor;

        public Gestor GestorACrear
        {
            get { return gestor; }
            set { SetProperty(ref gestor, value); }
        }

        private string contrasenyaRepetida;

        public string ContrasenyaRepetida
        {
            get { return contrasenyaRepetida; }
            set { SetProperty(ref contrasenyaRepetida, value); }
        }

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { SetProperty(ref mensaje, value); }
        }

        private bool errorActivo;

        public bool ErrorActivo
        {
            get { return errorActivo; }
            set { SetProperty(ref errorActivo, value); }
        }


        //Comando
        public RelayCommand EnviarGestorCommand { get; }

        public RegistrarseVM()
        {
            GestorACrear = new Gestor(0, "", "", "");
            EnviarGestorCommand = new RelayCommand(CrearGestor);
        }

        private void CrearGestor()
        {

            if (!GestorACrear.contrasenya.Equals(ContrasenyaRepetida))
            {
                Mensaje = "Las contraseñas no son iguales";
                ErrorActivo = true;
            }
            else
            {
                Regex regex = new Regex("^(?=.{10,}$)(?=(?:.*?[A-Z]){2})(?=.*?[a-z])(?=(?:.*?[0-9]){2}).*$");

                if (!regex.IsMatch(GestorACrear.contrasenya))
                {
                    Mensaje = "La contraseña debe contener al menos 2 mayúsculas, 1 minúscula, 2 dígitos y debe tener una longitud de al menos 10";
                    ErrorActivo = true;

                }
                else
                {
                    if (!EmailValido(GestorACrear.email))
                    {
                        Mensaje = "El email no es Valido";
                        ErrorActivo = true;
                    }
                    else
                    {
                        GestorACrear.contrasenya = Gestor.CifrarSha256(GestorACrear.contrasenya);
                        Mensaje = "";
                        ErrorActivo = false;
                        //Post de Gestor
                        switch (ApiService.CreateGestor(GestorACrear))
                        {
                            case System.Net.HttpStatusCode.Found:
                                Mensaje = "Ya existe una cuenta con el email introducido";
                                ErrorActivo = true;
                                break;

                            case System.Net.HttpStatusCode.Created:
                                Servicios.DialogosService.MensajeInformacion("Información", "Usted fue registrada/o");
                                break;

                            default:
                                Mensaje = "Estado no esperado, reintentalo";
                                ErrorActivo = true;
                                break;
                        }


                    }
                }

            }
        }

        public bool EmailValido(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            try
            {
                MailAddress mail = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

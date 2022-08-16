using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.GestorVM
{
    class CombatesUCVM : ObservableRecipient
    {
        private TorneoIndividual torneoIndividualSelecionado;

        public TorneoIndividual TorneoIndividualSelecionado
        {
            get { return torneoIndividualSelecionado; }
            set { SetProperty(ref torneoIndividualSelecionado, value); }
        }

        private ObservableCollection<CombatesIndividuales> combatesIndividuales;

        public ObservableCollection<CombatesIndividuales> CombatesIndividuales
        {
            get { return combatesIndividuales; }
            set { SetProperty(ref combatesIndividuales, value); }
        }

        private CombatesIndividuales combatesIndividualesSelecionado;

        public CombatesIndividuales CombatesIndividualesSelecionado
        {
            get { return combatesIndividualesSelecionado; }
            set
            {
                SetProperty(ref combatesIndividualesSelecionado, value);
                if (value != null)
                {
                    SolicitudesGanadoras[0] = value.idSolicitud1;
                    SolicitudesGanadoras[1] = value.idSolicitud2;
                }

            }
        }

        private ObservableCollection<Solicitudes> solicitudesGanadoras;

        public ObservableCollection<Solicitudes> SolicitudesGanadoras
        {
            get { return solicitudesGanadoras; }
            set { SetProperty(ref solicitudesGanadoras, value); }
        }


        private ObservableCollection<Solicitudes> listaDeSolicitantes;

        public ObservableCollection<Solicitudes> ListaDeSolicitantes
        {
            get { return listaDeSolicitantes; }
            set { SetProperty(ref listaDeSolicitantes, value); }
        }

        private bool editando;

        public bool Editando
        {
            get { return editando; }
            set { SetProperty(ref editando, value); }
        }



        public RelayCommand AbrirEditarCombateCommand { get; }
        public RelayCommand AbrirCrearCombateCommand { get; }
        public RelayCommand RefrecarCommand { get; }
        public RelayCommand CrearCombatesAleatoriosCommand { get; }
        public RelayCommand EliminarCommand { get; }
        public RelayCommand GuardarCambiosCommand { get; }
        public RelayCommand CancelarCommand { get; }

        public CombatesUCVM()
        {
            TorneoIndividualSelecionado = WeakReferenceMessenger.Default.Send<TorneoIndividualRequestEditarMessage>();
            SolicitudesGanadoras = new ObservableCollection<Solicitudes>
            {
                new Solicitudes(),
                new Solicitudes()
            };


            GetAllCombates();
            GetAllSolicitudesAcceptadas();

            AbrirEditarCombateCommand = new RelayCommand(EditarCombate);
            AbrirCrearCombateCommand = new RelayCommand(AbrirCrearCombate);
            RefrecarCommand = new RelayCommand(GetAllCombates);
            CrearCombatesAleatoriosCommand = new RelayCommand(CrearCombatesAleatorios);
            EliminarCommand = new RelayCommand(EliminarCombate);
            GuardarCambiosCommand = new RelayCommand(GuardarCambios);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void Cancelar()
        {
            Editando = false;
        }

        private void EliminarCombate()
        {
            if (CombatesIndividualesSelecionado != null)
            {
                if (DialogosService.MensajeInformacionDosOpciones("Confiramación","¿Seguro que desea eliminar el combate entre " + CombatesIndividualesSelecionado.idSolicitud1.nombreSolicitudes + " y " + CombatesIndividualesSelecionado.idSolicitud2.nombreSolicitudes +"?"))
                {
                    if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                    {
                        ApiService.DeleteCombatesindividuales(CombatesIndividualesSelecionado.idCombatesIndividuales, ApiService.LastElementoSeguridad);
                        GetAllCombates();
                    }
                    else
                    {
                        DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                    }
                }

            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccione el combate a eliminar");
            }


        }

        private void AbrirCrearCombate()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                NavegacionService.CrearConbatesDialogo();
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }

        public void EditarCombate()
        {
            if (CombatesIndividualesSelecionado != null)
            {
                if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                {
                    if (CombatesIndividualesSelecionado != null)
                    {
                        Editando = true;
                    }
                    else
                    {
                        DialogosService.MensajeError("Error", "Seleccioné el combate a editar");
                    }
                }
                else
                {
                    DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                }
            }
            else
            {
                DialogosService.MensajeError("Error", "Seleccione el combate a editar");
            }

        }

        public void GetAllSolicitudesAcceptadas()
        {
            ListaDeSolicitantes = ApiService.GetAllSolicitudesFromAcceptadasAndIdTorneo(TorneoIndividualSelecionado.idTorneoIndividual);
        }

        public void GetAllCombates()
        {
            CombatesIndividuales = ApiService.GetAllCombatesIndividualesFromIdTorneo(TorneoIndividualSelecionado.idTorneoIndividual);
        }

        public void CrearCombatesAleatorios()
        {
            bool opcionUsuario = true;


            if (ListaDeSolicitantes.Count < TorneoIndividualSelecionado.solicitudesMaximos)
            {
                opcionUsuario = DialogosService.MensajeInformacionDosOpciones("Confiramación", "Aun no hay un total de " + TorneoIndividualSelecionado.solicitudesMaximos + " participantes.\n¿Seguro que quiere crear los combates aleatorios");
            }

            if (opcionUsuario)
            {
                if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
                {
                    GetAllSolicitudesAcceptadas();
                    CombatesIndividuales combatesIndividuales = new CombatesIndividuales(0, null, null, null, TorneoIndividualSelecionado);
                    Random random = new Random();

                    //Ordena la lista aleatoriamente
                    for (int i = ListaDeSolicitantes.Count; i > 1; i--)
                    {
                        i--;
                        int k = random.Next(i + 1);
                        Solicitudes value = ListaDeSolicitantes[k];
                        ListaDeSolicitantes[k] = ListaDeSolicitantes[i];
                        ListaDeSolicitantes[i] = value;
                    }

                    for (int i = 0; i < ListaDeSolicitantes.Count; i++)
                    {
                        if ((ListaDeSolicitantes.Count - (i + 1)) >= 0)
                        {

                            if (combatesIndividuales.idSolicitud1 == null)
                            {
                                combatesIndividuales.idSolicitud1 = ListaDeSolicitantes[i];
                            }
                            else
                            {
                                combatesIndividuales.idSolicitud2 = ListaDeSolicitantes[i];
                                ApiService.CreateCombatesindividuales(combatesIndividuales, ApiService.LastElementoSeguridad);
                                combatesIndividuales = new CombatesIndividuales(0, null, null, null, TorneoIndividualSelecionado);
                            }
                        }
                    }

                    GetAllCombates();
                }
                else
                {
                    DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
                }
            }




        }

        public void GuardarCambios()
        {
            if ((ApiService.CheckApiKey(ApiService.LastElementoSeguridad)).validate)
            {
                ApiService.EditarCombatesindividualess(CombatesIndividualesSelecionado, ApiService.LastElementoSeguridad);
                Editando = false;
                GetAllCombates();
            }
            else
            {
                DialogosService.MensajeError("Sesión caducada", "Por cuestiones de seguridad antes debe de volver a iniciar sesión");
            }
        }
    }
}

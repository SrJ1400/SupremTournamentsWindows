using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SupremTournamentsProgram.Objetos;
using SupremTournamentsProgram.Servicios;
using System;
using System.Collections.ObjectModel;

namespace SupremTournamentsProgram.VM.SinLoguin
{
    class GeneralTorneoVM : ObservableObject
    {

        private ObservableCollection<TorneoIndividual> listaTorneos = new ObservableCollection<TorneoIndividual>();

        public ObservableCollection<TorneoIndividual> ListaTorneos
        {
            get { return listaTorneos; }
            set { SetProperty(ref listaTorneos, value); }
        }

        private TorneoIndividual torneoSelecionado;

        public TorneoIndividual TorneoSelecionado
        {
            get { return torneoSelecionado; }
            set { SetProperty(ref torneoSelecionado, value); }
        }

        private ObservableCollection<String> opcionesGet = new ObservableCollection<string>();

        public ObservableCollection<String> OpcionesGet
        {
            get { return opcionesGet; }
            set { SetProperty(ref opcionesGet, value); }
        }

        private Gets opcionTorneo;

        private string getSelecionado;

        public string GetSelecionado
        {
            get { return getSelecionado; }
            set
            {
                SetProperty(ref getSelecionado, value);
                BusquedadActiva = false;
                switch (value)
                {
                    case "Disponibles":
                        opcionTorneo = Gets.TorneoPorDisponibles;
                        break;

                    case "Fecha finalización ASC":
                        opcionTorneo = Gets.TorneoPorFechaFinAsc;
                        break;

                    case "Fecha finalización DESC":
                        opcionTorneo = Gets.TorneoPorFechaFinDesc;
                        break;

                    case "Menores y adultos":
                        opcionTorneo = Gets.TorneoPorMenoresTrue;
                        break;

                    case "Adultos":
                        opcionTorneo = Gets.TorneoPorMenoresFalse;
                        break;

                    case "Nivel Asc":
                        opcionTorneo = Gets.TorneoPorNivelAsc;
                        break;

                    case "Nivel Desc":
                        opcionTorneo = Gets.TorneoPorNivelDesc;
                        break;

                    case "Nombre Gestor":
                        opcionTorneo = Gets.TorneoPorNombreGestor;
                        break;

                    case "Nivel":
                        opcionTorneo = Gets.TorneoPorNivel;
                        break;

                    default:
                        opcionTorneo = Gets.TorneoPorDisponibles;
                        break;
                }
                RefrescarDatos();
            }
        }

        private string torneoABuscar;

        public string TorneoABuscar
        {
            get { return torneoABuscar; }
            set { SetProperty(ref torneoABuscar, value); }
        }

        private bool busquedadActiva;

        public bool BusquedadActiva
        {
            get { return busquedadActiva; }
            set { SetProperty(ref busquedadActiva, value); }
        }


        //Comandos
        public RelayCommand BuscarCommand { get; }
        public RelayCommand RefrescarCommand { get; }
        public GeneralTorneoVM()
        {
            OpcionesGet.Add("Disponibles");
            OpcionesGet.Add("Fecha finalización ASC");
            OpcionesGet.Add("Fecha finalización DESC");
            OpcionesGet.Add("Menores y adultos");
            OpcionesGet.Add("Adultos");
            OpcionesGet.Add("Nivel Asc");
            OpcionesGet.Add("Nivel Desc");
            OpcionesGet.Add("Nombre Gestor");
            OpcionesGet.Add("Nivel");

            GetSelecionado = OpcionesGet[0];


            //Mensajeria

            WeakReferenceMessenger.Default.Register<GeneralTorneoVM, TorneoIndividualRequestMessage>(this, (r, m) =>
            {
                m.Reply(r.TorneoSelecionado);//Proporciono la informacion (cuando lliegan mensages)
            });

            BuscarCommand = new RelayCommand(BuscarTorneo);

            RefrescarCommand = new RelayCommand(RefrescarDatos);
        }


        private void BuscarTorneo()
        {
            ObservableCollection<TorneoIndividual> torneosRecibido;
            switch (GetSelecionado)
            {
                case "Nombre Gestor":
                    if (!string.IsNullOrEmpty(TorneoABuscar))
                    {
                        torneosRecibido = ApiService.GetTorneosNombreGestor(TorneoABuscar);
                        if (torneosRecibido != null)
                        {
                            if (torneosRecibido.Count > 0)
                            {
                                ListaTorneos = torneosRecibido;
                            }
                            else
                            {
                                DialogosService.MensajeError("Error resultados", "No hay resultado con el gestor: " + TorneoABuscar);
                            }
                        }

                    }
                    else
                    {
                        DialogosService.MensajeError("Parametro necesario", "Es necesario rellenar el campo " + GetSelecionado);
                    }


                    break;

                case "Nivel":
                    try
                    {
                        if (!string.IsNullOrEmpty(TorneoABuscar))
                        {
                            torneosRecibido = ApiService.GetTorneosNivel(int.Parse(TorneoABuscar));
                            if (torneosRecibido != null)
                            {
                                if (torneosRecibido.Count > 0)
                                {
                                    ListaTorneos = torneosRecibido;
                                }
                                else
                                {
                                    DialogosService.MensajeError("Error resultados", "No hay resultado con el nivel: " + TorneoABuscar);
                                }
                            }
                        }
                        else
                        {
                            DialogosService.MensajeError("Parametro necesario", "Es necesario rellenar el campo " + GetSelecionado);
                        }
                    }
                    catch (Exception)
                    {
                        DialogosService.MensajeError("Parametro incorecto","Solo estan permitidos numeros en la busqueda por nivel");                        
                    }
                   
                    break;
            }
        }


        private void RefrescarDatos()
        {
            BusquedadActiva = false;
            switch (opcionTorneo)
            {
                case Gets.TorneoPorDisponibles:
                    GetTorneosDisponibles();
                    break;

                case Gets.TorneoPorFechaFinAsc:
                    GetTorneosFechaAsc();
                    break;

                case Gets.TorneoPorFechaFinDesc:
                    GetTorneosFechaDesc();
                    break;

                case Gets.TorneoPorMenoresTrue:
                    GetTorneosMenoresEdadTrue();
                    break;

                case Gets.TorneoPorMenoresFalse:
                    GetTorneosMenoresEdadFalse();
                    break;

                case Gets.TorneoPorNivelAsc:
                    GetTorneosNivelAsc();
                    break;

                case Gets.TorneoPorNivelDesc:
                    GetTorneosNivelDesc();
                    break;

                case Gets.TorneoPorNombreGestor:
                    BusquedadActiva = true;
                    break;

                case Gets.TorneoPorNivel:
                    BusquedadActiva = true;
                    break;

                default:
                    GetTorneosDisponibles();
                    break;
            }
            if (ListaTorneos != null)
            {
                TorneoSelecionado = ListaTorneos[0];
            }
        }

        private void GetTorneosDisponibles()
        {
            ListaTorneos = ApiService.GetTorneosDisponibles();
        }

        private void GetTorneosFechaAsc()
        {
            ListaTorneos = ApiService.GetTorneosFechaFinalizacionAsc();
        }

        private void GetTorneosFechaDesc()
        {
            ListaTorneos = ApiService.GetTorneosFechaFinalizacionDesc();
        }

        private void GetTorneosMenoresEdadTrue()
        {
            ListaTorneos = ApiService.GetTorneosMenorEdadTrue();
        }
        private void GetTorneosMenoresEdadFalse()
        {
            ListaTorneos = ApiService.GetTorneosMenorEdadFalse();
        }

        private void GetTorneosNivelAsc()
        {
            ListaTorneos = ApiService.GetTorneosNivelAsc();
        }

        private void GetTorneosNivelDesc()
        {
            ListaTorneos = ApiService.GetTorneosNivelDesc();
        }


        private void GetALLTorneos()
        {
            ListaTorneos = ApiService.GetALLTorneos();
        }

    }
}

using SupremTournamentsProgram.VM.GestorVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupremTournamentsProgram.Vistas.Gestor
{
    /// <summary>
    /// Lógica de interacción para CrearSolicitudAcceptada.xaml
    /// </summary>
    public partial class CrearSolicitudAcceptada : Window
    {
        DialogoCrearSolicitudAcceptadaVM vm;
        public CrearSolicitudAcceptada()
        {
            vm = new DialogoCrearSolicitudAcceptadaVM();
            this.DataContext = vm;
            InitializeComponent();
        }

        private void CrearSolicitudButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            vm.CrearSolicitud();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vm.AbrirDialogoImagen();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

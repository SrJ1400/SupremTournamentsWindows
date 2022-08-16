using SupremTournamentsProgram.VM;
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
    /// Lógica de interacción para CrearSolicitudContinua.xaml
    /// </summary>
    public partial class CrearSolicitudContinua : Window
    {
        DialogoCrearSolitudContinuaVM vm;

        public CrearSolicitudContinua()
        {
            InitializeComponent();
            vm = new DialogoCrearSolitudContinuaVM();
            this.DataContext = vm;
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            //Crear objeto en el Vm con Validaciones y hacer el post si todo esta bien estaria guapo mostrar una snakbar informando
            DialogResult = true;
            vm.CrearSolicitudContia();

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

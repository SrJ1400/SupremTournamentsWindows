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
    /// Lógica de interacción para AnadirSolicitudContinua.xaml
    /// </summary>
    public partial class AnadirSolicitudContinua : Window
    {
        DialogoAnadirSolicitudContinuaVM vm;
        public AnadirSolicitudContinua()
        {
            vm = new DialogoAnadirSolicitudContinuaVM();
            this.DataContext = vm;
            InitializeComponent();
        }

        private void Acceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            vm.GuardarComoSolicitudAcceptada();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

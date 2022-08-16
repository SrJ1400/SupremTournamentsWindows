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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupremTournamentsProgram.Vistas.Gestor
{
    /// <summary>
    /// Lógica de interacción para SolicitudesContinuasUC.xaml
    /// </summary>
    public partial class SolicitudesContinuasUC : UserControl
    {
        SolicitudesContinuasVM vm;

        public SolicitudesContinuasUC()
        {
            InitializeComponent();
            vm = new SolicitudesContinuasVM();
            this.DataContext = vm;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vm.AbrirDialogoImagen();
        }
    }
}

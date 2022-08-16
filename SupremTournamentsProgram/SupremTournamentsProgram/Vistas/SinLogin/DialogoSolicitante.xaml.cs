using SupremTournamentsProgram.VM;
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

namespace SupremTournamentsProgram.Vistas.SinLogin
{
    /// <summary>
    /// Lógica de interacción para FormularioSolicitanteASolicitar.xaml
    /// </summary>
    public partial class FormularioSolicitanteASolicitar : Window
    {
        DialogoSolicitanteVM vm;
        public FormularioSolicitanteASolicitar()
        {
            vm = new DialogoSolicitanteVM();
            this.DataContext = vm;
            InitializeComponent();
        }

        private void EnviarButton_Click(object sender, RoutedEventArgs e) 
        {
            DialogResult = true;
            vm.EnviarSolicitud();
            
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            vm.AbrirDialogoImagen();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

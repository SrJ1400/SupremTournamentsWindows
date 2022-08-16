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
    /// Lógica de interacción para Registrarse.xaml
    /// </summary>
    public partial class Registrarse : UserControl
    {
        RegistrarseVM vm;

        public Registrarse()
        {
            InitializeComponent();
            vm = new RegistrarseVM();
            this.DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.GestorACrear.contrasenya = ((PasswordBox)sender).Password;
        }

        private void PasswordBoxRepetida_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.ContrasenyaRepetida = ((PasswordBox)sender).Password;
        }
    }
}

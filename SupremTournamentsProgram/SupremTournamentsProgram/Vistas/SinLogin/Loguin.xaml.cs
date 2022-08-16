using SupremTournamentsProgram.VM;
using SupremTournamentsProgram.VM.SinLoguin;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        LoguinVM vm;

        public Login()
        {
            InitializeComponent();
            vm = new LoguinVM();
            this.DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.Contrasenya = ((PasswordBox)sender).Password;
        }
    }
}

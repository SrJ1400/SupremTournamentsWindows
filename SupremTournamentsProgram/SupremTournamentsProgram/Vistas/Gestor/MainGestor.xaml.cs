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

namespace SupremTournamentsProgram.Vistas.Gestor
{
    /// <summary>
    /// Lógica de interacción para MainGestor.xaml
    /// </summary>
    public partial class MainGestor : UserControl
    {
        MainGestorVM vm;
        public MainGestor()
        {
            InitializeComponent();
            vm = new MainGestorVM();
            this.DataContext = vm;
        }
    }
}

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
    /// Lógica de interacción para EditarTornoIndividual.xaml
    /// </summary>
    public partial class MainEditarTornoIndividual : UserControl
    {
        MainEditarTorneoVM vm;
        public MainEditarTornoIndividual()
        {
            vm = new MainEditarTorneoVM();
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}

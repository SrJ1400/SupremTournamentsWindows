using SupremTournamentsProgram.Servicios;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupremTournamentsProgram.Vistas.SinLogin
{
    /// <summary>
    /// Lógica de interacción para GeneralTorneo.xaml
    /// </summary>
    public partial class GeneralTorneo : UserControl
    {
        GeneralTorneoVM vm;
        public GeneralTorneo()
        {
            vm = new GeneralTorneoVM();
            this.DataContext = vm;
            InitializeComponent();
        }

        private void Card_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavegacionService.VerTorneoIndividual();
        }
    }
}

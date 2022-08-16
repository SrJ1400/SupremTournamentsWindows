using SupremTournamentsProgram.Servicios;
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
    /// Lógica de interacción para MainInfoTorneoIndividual.xaml
    /// </summary>
    public partial class MainInfoTorneoIndividual : UserControl
    {
        MainInfoTorneoIndividualVM vm;

        public MainInfoTorneoIndividual()
        {
            InitializeComponent();
            vm = new MainInfoTorneoIndividualVM();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavegacionService.VerHomePage();
        }
    }
}

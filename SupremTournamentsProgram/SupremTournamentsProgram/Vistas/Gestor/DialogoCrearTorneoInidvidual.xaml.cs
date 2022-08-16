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
    /// Lógica de interacción para DialogoCrearTorneoInidvidual.xaml
    /// </summary>
    public partial class DialogoCrearTorneoInidvidual : Window
    {
        DialogoCrearTorneoIndividualVM vm;
        public DialogoCrearTorneoInidvidual()
        {   
            vm = new DialogoCrearTorneoIndividualVM();
            this.DataContext = vm;
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.CrearTorneoIndividual();
            DialogResult = true;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            vm.AbrirDialogoImagen();
        }

        private void CustomRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            vm.TorneoIndividualACrear.nivel = e.NewValue;
        }

        private void Cacelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

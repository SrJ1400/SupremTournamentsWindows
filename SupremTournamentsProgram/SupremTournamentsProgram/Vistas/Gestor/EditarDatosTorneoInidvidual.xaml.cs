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
    /// Lógica de interacción para EditarDatosTorneoInidvidual.xaml
    /// </summary>
    public partial class EditarDatosTorneoInidvidual : UserControl
    {
        EditarDatosTorneoInidvidualVM vm;
        public EditarDatosTorneoInidvidual()
        {
            vm = new EditarDatosTorneoInidvidualVM();
            this.DataContext = vm;
            InitializeComponent();
        }


        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            vm.AbrirDialogoImagen();
        }

        private void CustomRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            vm.TorneoIndividualAEditar.nivel = e.NewValue;
        }
    }
}

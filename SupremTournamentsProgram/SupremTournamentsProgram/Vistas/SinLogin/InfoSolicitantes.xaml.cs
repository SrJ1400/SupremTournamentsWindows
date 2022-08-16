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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupremTournamentsProgram.Vistas.SinLogin
{
    /// <summary>
    /// Lógica de interacción para InfoSolicitantes.xaml
    /// </summary>
    public partial class InfoSolicitantes : UserControl
    {
        InfoSolicitanteVM vm;

        public InfoSolicitantes()
        {
            InitializeComponent();
            vm = new InfoSolicitanteVM();
            this.DataContext = vm;
        }
    }
}

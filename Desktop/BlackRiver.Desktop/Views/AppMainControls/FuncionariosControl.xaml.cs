using BlackRiver.Desktop.Extensions;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for FuncionariosControl.xaml
    /// </summary>
    public partial class FuncionariosControl : UserControl, IControlUpdate
    {
        public FuncionariosControl()
        {
            InitializeComponent();
        }

        public void UpdateControlData()
        {
            //TODO: refresh data
        }

        private void btnAddFuncionario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarFuncionarioWindow().SafeShowDialog(null);
        }

        private void btnEditarFuncionario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new EditarFuncionarioWindow().SafeShowDialog(null);
        }
    }
}

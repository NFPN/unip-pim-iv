using BlackRiver.Desktop.Extensions;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EstoqueControl.xaml
    /// </summary>
    public partial class EstoqueControl : UserControl, IControlUpdate
    {
        public EstoqueControl()
        {
            InitializeComponent();
        }

        public void UpdateControlData()
        {
            //TODO: refresh data
        }

        private void btnAddProduto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarProdutoWindow().SafeShowDialog();
        }

        private void btnEditarProduto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new EditarProdutoWindow().SafeShowDialog();
        }
    }
}

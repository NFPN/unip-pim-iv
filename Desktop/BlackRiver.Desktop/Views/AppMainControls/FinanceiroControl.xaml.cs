using BlackRiver.Desktop.Extensions;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for FinanceiroControl.xaml
    /// </summary>
    public partial class FinanceiroControl : UserControl, IControlUpdate
    {
        public FinanceiroControl()
        {
            InitializeComponent();
        }

        public void UpdateControlData()
        {
            //TODO: refresh data
        }

        private void btnAddPagamento_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new PagamentoWindow().SafeShowDialog(null);
        }
    }
}

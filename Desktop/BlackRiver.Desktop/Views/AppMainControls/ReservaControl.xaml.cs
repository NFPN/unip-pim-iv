using BlackRiver.Desktop.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ReservaControl.xaml
    /// </summary>
    public partial class ReservaControl : UserControl
    {
        public ReservaControl()
        {
            InitializeComponent();
        }

        private void btnAddReserva_Click(object sender, RoutedEventArgs e)
        {
            new CriarReservaWindow().SafeShowDialog();
        }

        private void btnEditarReserva_Click(object sender, RoutedEventArgs e)
        {
            new EditarReservaWindow().SafeShowDialog();
        }

        private void txtBoxReservaSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

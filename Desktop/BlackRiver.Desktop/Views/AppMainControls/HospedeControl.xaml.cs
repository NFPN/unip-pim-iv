using BlackRiver.Desktop.Extensions;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for HospedeControl.xaml
    /// </summary>
    public partial class HospedeControl : UserControl, IControlUpdate
    {
        public HospedeControl()
        {
            InitializeComponent();
        }

        public void UpdateControlData()
        {
            //TODO: refresh data
        }

        private void btnNovoHospede_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarHospedeWindow().SafeShowDialog(null);
        }

        private void btnEditarHospede_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new EditarHospedeWindow().SafeShowDialog(null);
        }
    }
}

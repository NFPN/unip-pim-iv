using BlackRiver.Desktop.Extensions;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarHospedeWindow.xaml
    /// </summary>
    public partial class CriarHospedeWindow : Window
    {
        public CriarHospedeWindow()
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
        }

        private void btnNovoHospedeCadastrar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Create with API call
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

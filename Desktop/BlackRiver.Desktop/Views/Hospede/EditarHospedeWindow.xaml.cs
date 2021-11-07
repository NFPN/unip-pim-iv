using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarHospedeWindow.xaml
    /// </summary>
    public partial class EditarHospedeWindow : Window
    {
        public EditarHospedeWindow(object[] arguments = null)
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditHospedeAtualizar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Update with API call
        }

        private void btnEditHospedeRemover_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Remove with API call
        }
    }
}

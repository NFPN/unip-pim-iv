using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarQuartoWindow.xaml
    /// </summary>
    public partial class CriarQuartoWindow : Window
    {
        public CriarQuartoWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddQuartoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: post to api
        }
    }
}

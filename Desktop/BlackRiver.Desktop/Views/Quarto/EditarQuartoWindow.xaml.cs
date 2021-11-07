using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarQuartoWindow.xaml
    /// </summary>
    public partial class EditarQuartoWindow : Window
    {
        public EditarQuartoWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnEditQuartoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: get quarto from api
            //update quarto
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

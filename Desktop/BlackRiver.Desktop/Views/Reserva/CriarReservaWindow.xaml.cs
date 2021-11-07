using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarReservaWindow.xaml
    /// </summary>
    public partial class CriarReservaWindow : Window
    {
        public CriarReservaWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNovaReservaCriar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Create with API call
        }
    }
}

using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarReservaWindow.xaml
    /// </summary>
    public partial class EditarReservaWindow : Window
    {
        public EditarReservaWindow(object[] arguments = null)
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

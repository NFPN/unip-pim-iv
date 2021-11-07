using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for LoggedAreaWindow.xaml
    /// </summary>
    public partial class LoggedAreaWindow : Window
    {
        public LoggedAreaWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

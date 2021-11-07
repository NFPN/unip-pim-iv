using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarFuncionarioWindow.xaml
    /// </summary>
    public partial class EditarFuncionarioWindow : Window
    {
        public EditarFuncionarioWindow(object[] arguments = null)
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditFunc_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Post to api update
        }
    }
}

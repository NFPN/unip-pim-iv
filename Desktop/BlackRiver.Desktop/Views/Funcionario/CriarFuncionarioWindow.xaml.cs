using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarFuncionarioWindow.xaml
    /// </summary>
    public partial class CriarFuncionarioWindow : Window
    {
        public CriarFuncionarioWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddFunc_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Post to api
        }
    }
}

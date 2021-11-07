using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for PagamentoWindow.xaml
    /// </summary>
    public partial class PagamentoWindow : Window
    {
        public PagamentoWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPagamentoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            //TODO:Post to api
        }

        public string ShowDialog(object data)
        {
            var test = ShowDialog();

            return test.ToString();
        }
    }
}

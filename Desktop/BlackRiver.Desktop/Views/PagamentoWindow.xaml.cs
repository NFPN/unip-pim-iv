using BlackRiver.Desktop.Extensions;
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
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
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

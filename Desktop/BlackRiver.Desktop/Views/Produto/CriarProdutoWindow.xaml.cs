using BlackRiver.Desktop.Extensions;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarProdutoWindow.xaml
    /// </summary>
    public partial class CriarProdutoWindow : Window
    {
        public CriarProdutoWindow()
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnProdutoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Post to api
        }
    }
}

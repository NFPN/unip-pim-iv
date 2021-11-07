using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarProdutoWindow.xaml
    /// </summary>
    public partial class EditarProdutoWindow : Window
    {
        public EditarProdutoWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditProdutoAtualizar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: post to api
        }
    }
}

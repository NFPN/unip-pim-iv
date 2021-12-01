using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarProdutoWindow.xaml
    /// </summary>
    public partial class EditarProdutoWindow : Window
    {
        public Produto Produto { get; set; }

        public EditarProdutoWindow(Produto produto = null)
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            Produto = produto;
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

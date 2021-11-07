using System.Windows;

namespace BlackRiver.Desktop.Views.Login
{
    /// <summary>
    /// Interaction logic for NewPasswordEditWindow.xaml
    /// </summary>
    public partial class NewPasswordEditWindow : Window
    {
        public NewPasswordEditWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            //TODO: reset password and show login screen
        }
    }
}

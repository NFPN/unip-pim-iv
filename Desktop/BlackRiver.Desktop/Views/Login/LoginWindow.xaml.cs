using BlackRiver.Desktop.Extensions;
using BlackRiver.Desktop.Views.Login;
using System;
using System.Windows;
using System.Windows.Input;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainLoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            BringIntoView();
            _ = Focus();

            MouseDown += delegate { this.SafeDragMove(); };
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var admin = "admin";

                if (!await BlackRiverAPI.FetchToken(txtBoxUsername.Text, txtBoxPassword.Password))
                    return;

                var user = await BlackRiverAPI.GetLoggedUser();
                var userType = (LoginTypes)user.Type;

                if (user.Username.Equals(admin, StringComparison.Ordinal) && user.Password.Equals(admin, StringComparison.Ordinal))
                {
                    new NewPasswordEditWindow(user).Show();
                    Close();
                    return;
                }

                if (userType is LoginTypes.Customer or LoginTypes.None && !user.Password.Equals(txtBoxPassword.Password))
                {
                    BlackRiverExtensions.ShowMessage("Usuário inválido", "Erro");
                    return;
                }

                BlackRiverGlobal.IsAdminLogin = userType == LoginTypes.Manager;
                var mainWindow = new LoggedAreaWindow();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                BlackRiverExtensions.ShowMessage(ex.Message, "Erro");
            }
        }

        private void txtBlockForgotPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _ = new NewPasswordEditWindow(new() { Username = txtBoxUsername.Text }).ShowDialog();
            return;
        }

        private void txtBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                btnLogin_Click(sender, e);
        }
    }
}

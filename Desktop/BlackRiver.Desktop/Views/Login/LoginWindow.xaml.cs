using BlackRiver.Desktop.Extensions;
using BlackRiver.Desktop.Views.Login;
using BlackRiver.EntityModels;
using Newtonsoft.Json;
using System.IO;
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
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
            BringIntoView();
            _ = Focus();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var admin = "admin";

            if (!File.Exists(BlackRiverGlobal.FirstUseFile) && txtBoxUsername.Text.Equals(admin) && txtBoxPassword.Password.Equals(admin))
            {
                _ = Directory.CreateDirectory(BlackRiverGlobal.FirstUseFolder);
                new NewPasswordEditWindow(admin).Show();
                Close();
                return;
            }

            var tokenResponse = await BlackRiverGlobal.APIClient
                .GetAsync(BlackRiverGlobal.LoginUri + $"?username={txtBoxUsername.Text}&password={txtBoxPassword.Password}");

            if (!tokenResponse.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Login Incorreto", "Error");
                return;
            }

            var token = JsonConvert.DeserializeObject<APIToken>(await tokenResponse.Content.ReadAsStringAsync());
            BlackRiverGlobal.LoginToken = token.Token;

            var userResponse = await BlackRiverGlobal.APIClient
                .GetAsync(BlackRiverGlobal.AuthUserUri);

            if (!userResponse.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar usuário", "Error");
                return;
            }

            var user = JsonConvert.DeserializeObject<UserLogin>(await userResponse.Content.ReadAsStringAsync());
            BlackRiverGlobal.IsAdminLogin = ((LoginTypes)user.Type) == LoginTypes.Manager;

            var mainWindow = new LoggedAreaWindow();
            mainWindow.Show();
            Close();
        }

        private void txtBlockForgotPassword_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var resetAdminPassword = new NewPasswordEditWindow(txtBoxUsername.Text).ShowDialog();
            return;
        }

        private void txtBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                btnLogin_Click(sender, e);
        }
    }
}

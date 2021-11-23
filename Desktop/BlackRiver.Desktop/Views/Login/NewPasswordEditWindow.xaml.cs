using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System.IO;
using System.Net.Http.Json;
using System.Windows;

namespace BlackRiver.Desktop.Views.Login
{
    /// <summary>
    /// Interaction logic for NewPasswordEditWindow.xaml
    /// </summary>
    public partial class NewPasswordEditWindow : Window
    {
        public NewPasswordEditWindow(string user = null)
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();

            txtBoxUser.Text = user;

            if (user.Equals("admin", System.StringComparison.Ordinal))
                txtBoxOldPassword.Password = user;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }

        private async void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!txtBoxPassword.Password.Equals(txtBoxConfirmPassword.Password, System.StringComparison.Ordinal))
            {

                BlackRiverExtensions.ShowMessage("A senha de confirmação não é a mesma", "Erro");
                return;
            }

            if (File.Exists(BlackRiverGlobal.FirstUseFile))
            {
                var resetResponse = await BlackRiverAPI.Client.GetAsync(BlackRiverAPI.UpdateLoginUri + $"?username={txtBoxUser.Text}&password={txtBoxPassword.Password}");

                if (resetResponse.IsSuccessStatusCode)
                    Close();

                return;
            }

            var adminLogin = new UserLogin
            {
                Username = txtBoxUser.Text,
                Password = txtBoxPassword.Password,
                Type = 1,
            };

            var postResponse = await BlackRiverAPI.Client.PostAsJsonAsync(BlackRiverAPI.RegisterUri + $"?isCustomer={false}", adminLogin);

            if (postResponse.IsSuccessStatusCode)
            {
                using FileStream file = File.Create(BlackRiverGlobal.FirstUseFile);
                var login = new LoginWindow();
                login.Show();
                Close();
            }

            var error = new BlackRiverMessageWindow("Não pode criar Admin", "Erro");

            if (error.IsLoaded)
                error.ShowDialog();
        }
    }
}

using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System.Windows;

namespace BlackRiver.Desktop.Views.Login
{
    /// <summary>
    /// Interaction logic for NewPasswordEditWindow.xaml
    /// </summary>
    public partial class NewPasswordEditWindow : Window
    {
        public NewPasswordEditWindow(UserLogin user = null)
        {
            InitializeComponent();

            MouseDown += delegate { this.SafeDragMove(); };

            txtBoxUser.Text = user?.Username ?? string.Empty;
            txtBoxPassword.Password = user?.Password ?? string.Empty;
            txtBoxOldPassword.Password = user?.Password ?? string.Empty;

            if (user.Password == "admin")
            {
                txtBoxUser.IsEnabled = false;
                txtBoxOldPassword.IsEnabled = false;
            }

            UpdateLayout();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            BlackRiverExtensions.ShowMessage("Ação indisponível no momento", "Erro");
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

            //login with oldpassword
            //get funcionario
            //update funcionario

            var resetResponse = await BlackRiverAPI.Client
                .PostAsync(BlackRiverAPI.UpdateLoginUri + @$"?username={txtBoxUser.Text}&password={txtBoxPassword.Password}", null);

            if (resetResponse.IsSuccessStatusCode)
            {
                new LoginWindow().Show();
                Close();
            }
            else if (resetResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                BlackRiverExtensions.ShowMessage("Não pode criar Admin", "Erro");
        }
    }
}

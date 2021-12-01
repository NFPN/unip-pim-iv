using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarHospedeWindow.xaml
    /// </summary>
    public partial class CriarHospedeWindow : Window
    {
        public CriarHospedeWindow()
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
        }

        private async void btnNovoHospedeCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = txtBoxNovoHospedeEmail.Text;
                var password = !string.IsNullOrEmpty(txtBoxNovoHospedeCPF.Text) ? txtBoxNovoHospedeCPF.Text : txtBoxNovoHospedeCNPJ.Text;

                var login = await BlackRiverAPI.Register(email, password, 4);

                var idade = int.Parse(txtBoxNovoHospedeIdade.Text);

                var hospede = new Hospede
                {
                    Nome = txtBoxNovoHospedeNome.Text,
                    CNPJ = txtBoxNovoHospedeCNPJ.Text,
                    CPF = txtBoxNovoHospedeCPF.Text,
                    DataNascimento = DateTime.Today.AddYears(-idade).ToUniversalTime(),
                    Email = email,
                    LoginId = login.Id,
                    RG = txtBoxNovoHospedeRG.Text,
                    Telefone = txtBoxNovoHospedeTel.Text,
                };

                var result = await BlackRiverAPI.CreateHospede(hospede);

                if (result == null)
                    return;

                BlackRiverExtensions.ShowMessage("Hóspede criado", "Sucesso");
                Close();
            }
            catch (Exception)
            {
            }
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

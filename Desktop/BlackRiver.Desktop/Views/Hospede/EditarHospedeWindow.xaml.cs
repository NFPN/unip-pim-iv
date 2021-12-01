using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarHospedeWindow.xaml
    /// </summary>
    public partial class EditarHospedeWindow : Window
    {
        public Hospede HospedeData { get; set; }

        public EditarHospedeWindow(Hospede hospede = null)
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            HospedeData = hospede;
            UpdateControlData();
        }

        private void UpdateControlData()
        {
            if (HospedeData == null)
                return;

            var zeroTime = new DateTime(1, 1, 1);
            var span = DateTime.UtcNow.Subtract(HospedeData.DataNascimento);

            // Because we start at year 1 for the Gregorian
            // calendar, we must subtract a year here.
            int idade = (zeroTime + span).Year - 1;

            txtBoxEditHospedeEmail.Text = HospedeData.Email;
            txtBoxEditHospedeNome.Text = HospedeData.Nome;
            txtBoxEditHospedeTel.Text = HospedeData.Telefone;
            txtBoxEditHospedeRG.Text = HospedeData.RG;
            txtBoxEditHospedeCPF.Text = HospedeData.CPF;
            txtBoxNovoHospedeIdade.Text = idade.ToString();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnEditHospedeAtualizar_Click(object sender, RoutedEventArgs e)
        {
            var idade = int.Parse(txtBoxNovoHospedeIdade.Text);

            HospedeData = new Hospede
            {
                Id = HospedeData.Id,
                Nome = txtBoxEditHospedeNome.Text,
                CNPJ = txtBoxEditHospedeCNPJ.Text,
                CPF = txtBoxEditHospedeCPF.Text,
                DataNascimento = DateTime.UtcNow.AddYears(-idade),
                Email = txtBoxEditHospedeEmail.Text,
                LoginId = HospedeData.LoginId,
                RG = txtBoxEditHospedeRG.Text,
                Telefone = txtBoxEditHospedeTel.Text,
            };

            var result = await BlackRiverAPI.UpdateHospede(HospedeData);

            if (result == null)
                return;

            Close();
        }

        private void btnEditHospedeRemover_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Remove with API call
        }
    }
}

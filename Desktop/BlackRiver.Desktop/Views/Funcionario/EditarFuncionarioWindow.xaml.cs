using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarFuncionarioWindow.xaml
    /// </summary>
    public partial class EditarFuncionarioWindow : Window
    {
        public Funcionario EditFuncionario { get; private set; }

        public EditarFuncionarioWindow(Funcionario funcionario = null)
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            EditFuncionario = funcionario;

            txtBoxEditFuncCarteira.Text = EditFuncionario.CTPS;
            txtBoxEditFuncCPF.Text = EditFuncionario.CPF;
            txtBoxEditFuncEmail.Text = EditFuncionario.Email;
            txtBoxEditFuncEndereco.Text = EditFuncionario.Endereco;
            txtBoxEditFuncNome.Text = EditFuncionario.Nome;
            txtBoxEditFuncRG.Text = EditFuncionario.RG;
            txtBoxEditFuncTel.Text = EditFuncionario.Telefone;

            comboEditFuncCargo.SelectedItem = Enum.Parse<Cargos>(EditFuncionario.Cargo);
            comboEditFuncDept.SelectedItem = Enum.Parse<Departamentos>(EditFuncionario.Departamento);

            checkEditFuncionarioAtivo.IsChecked = EditFuncionario.Ativo;

            UpdateControlData();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditFunc_Click(object sender, RoutedEventArgs e)
        {
            var result = BlackRiverAPI.UpdateFuncionario(EditFuncionario);

            if (result != null)
                BlackRiverExtensions.ShowMessage("Editado com sucesso", "Erro");

            Close();
        }

        public async void UpdateControlData()
        {
            foreach (var item in Enum.GetValues(typeof(Departamentos)))
                comboEditFuncDept.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(Cargos)))
                comboEditFuncCargo.Items.Add(item);

            foreach (var item in await BlackRiverAPI.ExternalGetEstados())
                comboFuncionarioEstado.Items.Add(item.Sigla);

            foreach (var item in await BlackRiverAPI.ExternalGetCidades(comboFuncionarioEstado.Items[0].ToString()))
                comboFuncionarioCidade.Items.Add(item.Nome);

            comboEditFuncDept.UpdateLayout();
            comboEditFuncCargo.UpdateLayout();
            comboFuncionarioEstado.UpdateLayout();
        }

        private async void comboFuncionarioEstado_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            comboFuncionarioCidade.Items.Clear();
            foreach (var cidade in await BlackRiverAPI.ExternalGetCidades(comboFuncionarioEstado.SelectedValue.ToString()))
            {
                if (comboFuncionarioCidade.Items.Count == 1)
                    comboFuncionarioCidade.SelectedValue = comboFuncionarioCidade.Items[0];

                comboFuncionarioCidade.Items.Add(cidade.Nome);
                comboFuncionarioCidade.UpdateLayout();
            }
        }


    }
}

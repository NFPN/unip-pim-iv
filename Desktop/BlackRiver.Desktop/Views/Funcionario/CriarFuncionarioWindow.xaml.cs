using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Linq;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarFuncionarioWindow.xaml
    /// </summary>
    public partial class CriarFuncionarioWindow : Window
    {
        public CriarFuncionarioWindow()
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };

            comboFuncionarioEstado.SelectedItem = "SP";
            comboFuncionarioCidade.SelectedItem = "São José do Rio Preto";
            UpdateControlData();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAddFunc_Click(object sender, RoutedEventArgs e)
        {
            var newFuncionaro = new Funcionario
            {
                Cargo = comboAddFuncCargo.SelectedItem.ToString(),
                Departamento = comboAddFuncDept.SelectedItem.ToString(),
                CPF = txtBoxAddFuncCPF.Text,
                CTPS = txtBoxAddFuncCarteira.Text,
                DataNascimento = dateAddFuncDataNasc.SelectedDate.GetValueOrDefault().ToUniversalTime(),
                Email = txtBoxAddFuncEmail.Text,
                Endereco = txtBoxAddFuncEndereco.Text,
                Nome = txtBoxAddFuncNome.Text,
                RG = txtBoxAddFuncRG.Text,
                HotelId = 1,
                Ativo = true,
                Id = 0,
            };

            var municipios = await BlackRiverAPI.GetMunicipios();

            if (municipios.Any(m => m.Nome.Equals(comboFuncionarioCidade.SelectedItem.ToString())))
                newFuncionaro.MunicipioId = municipios.FirstOrDefault(m => m.Nome.Equals(comboFuncionarioCidade.SelectedItem.ToString())).Id;
            else
            {
                var mun = await BlackRiverAPI.CreateMunicipio(new Municipio
                {
                    Nome = comboFuncionarioCidade.SelectedItem.ToString(),
                    UF = comboFuncionarioEstado.SelectedItem.ToString(),
                });

                newFuncionaro.MunicipioId = mun.Id;
            }

            var tipo = (Cargos)comboAddFuncCargo.SelectedItem == Cargos.GerenteGeral ? (int)LoginTypes.Manager : (int)LoginTypes.Employee;

            try
            {
                var user = await BlackRiverAPI.Register(newFuncionaro.Email, newFuncionaro.CPF, tipo);
                newFuncionaro.LoginId = user.Id;
            }
            catch (Exception ex)
            {
                return;
            }

            var result = await BlackRiverAPI.CreateFuncionario(newFuncionaro);

            if (result != null)
                Close();
        }

        public async void UpdateControlData()
        {
            foreach (var item in Enum.GetValues(typeof(Departamentos)))
                comboAddFuncDept.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(Cargos)))
                comboAddFuncCargo.Items.Add(item);

            foreach (var item in await BlackRiverAPI.ExternalGetEstados())
                comboFuncionarioEstado.Items.Add(item.Sigla);

            foreach (var item in await BlackRiverAPI.ExternalGetCidades(comboFuncionarioEstado.Items[0].ToString()))
                comboFuncionarioCidade.Items.Add(item.Nome);

            comboAddFuncDept.UpdateLayout();
            comboAddFuncCargo.UpdateLayout();
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

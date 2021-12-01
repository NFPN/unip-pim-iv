using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for FuncionariosControl.xaml
    /// </summary>
    public partial class FuncionariosControl : UserControl, IControlUpdate
    {
        private List<Funcionario> funcionarioList;
        private List<FuncionarioDataRow> funcionarioDataViewList = new();

        public FuncionariosControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            funcionarioList = await BlackRiverAPI.GetFuncionarios();

            datagridFuncionarios.UnselectAllCells();
            datagridFuncionarios.ItemsSource = null;
            datagridFuncionarios.UpdateLayout();
            funcionarioDataViewList.Clear();
            datagridFuncionarios.ItemsSource = funcionarioDataViewList;

            foreach (var funcionario in funcionarioList)
            {
                var funcionarioRow = new FuncionarioDataRow
                {
                    Nome = funcionario.Nome,
                    Cargo = funcionario.Cargo,
                    Departamento = funcionario.Departamento,
                    Email = funcionario.Email,
                };

                funcionarioDataViewList.Add(funcionarioRow);
            }
            datagridFuncionarios.UpdateLayout();
        }

        private async void btnAddFuncionario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarFuncionarioWindow().SafeShowDialog();
            await Application.Current.Dispatcher.Invoke(async delegate
            {
                await Task.Delay(1000);
                UpdateControlData();
                UpdateLayout();
            });
        }

        private async void btnEditarFuncionario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var row = datagridFuncionarios.SelectedItems[0];
            var index = datagridFuncionarios.Items.IndexOf(row);

            new EditarFuncionarioWindow(funcionarioList[index]).SafeShowDialog();
            await Application.Current.Dispatcher.Invoke(async delegate
            {
                await Task.Delay(1000);
                UpdateControlData();
                UpdateLayout();
            });
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateControlData();
        }
    }
}

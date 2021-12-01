using BlackRiver.Desktop.Extensions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for LoggedAreaWindow.xaml
    /// </summary>
    public partial class LoggedAreaWindow : Window
    {
        private readonly Dictionary<ContentType, UserControl> controls = new()
        {
            { ContentType.Dashboard, new DashboardControl() },
            { ContentType.Hospedes, new HospedeControl() },
            { ContentType.Quartos, new QuartoControl() },
            { ContentType.Estoque, new EstoqueControl() },
            { ContentType.Financeiro, new FinanceiroControl() },
            { ContentType.Reservas, new ReservaControl() },
        };

        private readonly SolidColorBrush defaultButtonBrush = new((Color)ColorConverter.ConvertFromString("#FF545454"));
        private Button lastSenderButton;

        public LoggedAreaWindow()
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            SetControlButtons();
            SwitchContent(ContentType.Dashboard);
            lastSenderButton = DashboardButton;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void DashboardButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Dashboard, sender);

        private void HospedeButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Hospedes, sender);

        private void QuartosButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Quartos, sender);

        private void EstoqueButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Estoque, sender);

        private void FinanceiroButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Financeiro, sender);

        private void FuncionariosButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Funcionarios, sender);

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Relatorios, sender);

        private void HotelButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Hotel, sender);

        private void ReservaButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Reservas, sender);

        private void SetControlButtons()
        {
            if (BlackRiverGlobal.IsAdminLogin)
            {
                controls.Add(ContentType.Funcionarios, new FuncionariosControl());
                controls.Add(ContentType.Relatorios, new RelatoriosControl());
                controls.Add(ContentType.Hotel, new HotelControl());
            }
            else
            {
                FuncionariosButton.Visibility = Visibility.Collapsed;
                //RelatoriosButton.Visibility = Visibility.Collapsed;
                HotelButton.Visibility = Visibility.Collapsed;
            }
        }

        private void SwitchContent(ContentType content, object sender = null)
        {
            if (ControlGrid.Children.Contains(controls[content]))
                return;

            if (sender != null)
                ChangeButtonColor((Button)sender);

            var control = controls[content];

            ControlGrid.Children.Clear();
            _ = ControlGrid.Children.Add(control);

            if (control is IControlUpdate controlUpdate)
                controlUpdate.UpdateControlData();
        }

        private void ChangeButtonColor(Button button)
        {
            lastSenderButton.Background = defaultButtonBrush;
            lastSenderButton.Foreground = Brushes.White;

            button.Background = Brushes.Gold;
            button.Foreground = Brushes.Black;

            lastSenderButton = button;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }
    }
}

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
            { ContentType.Funcionarios, new FuncionariosControl() },
            { ContentType.Relatorios, new RelatoriosControl() },
            { ContentType.Hotel, new HotelControl() },
        };

        private Button lastSenderButton;
        private SolidColorBrush defaultButtonBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF545454"));

        public LoggedAreaWindow()
        {
            InitializeComponent();
            SwitchContent(ContentType.Dashboard);
            MouseDown += delegate { SafeDragMove(); };
            lastSenderButton = DashboardButton;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e) => Close();

        private void DashboardButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Dashboard, sender);

        private void HospedeButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Hospedes, sender);

        private void QuartosButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Quartos, sender);

        private void EstoqueButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Estoque, sender);

        private void FinanceiroButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Financeiro, sender);

        private void FuncionariosButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Funcionarios, sender);

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Relatorios, sender);

        private void HotelButton_Click(object sender, RoutedEventArgs e) => SwitchContent(ContentType.Hotel, sender);

        private void SwitchContent(ContentType content, object sender = null)
        {
            if (ControlGrid.Children.Contains(controls[content]))
                return;

            if (sender != null)
            {
                var button = (Button)sender;

                lastSenderButton.Background = defaultButtonBrush;
                lastSenderButton.Foreground = Brushes.White;

                button.Background = Brushes.Gold;
                button.Foreground = Brushes.Black;

                lastSenderButton = button;
            }

            ControlGrid.Children.Clear();
            ControlGrid.Children.Add(controls[content]);
        }

        public void SafeDragMove()
        {
            try
            {
                DragMove();
            }
            catch { }
        }
    }
}

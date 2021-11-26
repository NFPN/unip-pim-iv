using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class BlackRiverMessageWindow : Window
    {
        public string Message { get; }
        public string WindowTitle { get; }

        public BlackRiverMessageWindow(string message = null, string title = null)
        {
            InitializeComponent();
            Message = message;
            WindowTitle = title;
            Loaded += SetupInfo;
        }

        private void SetupInfo(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Message))
                txtBlockMessage.Text = Message;

            if (!string.IsNullOrWhiteSpace(WindowTitle))
                Title = WindowTitle;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

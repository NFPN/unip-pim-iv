using System;
using System.ComponentModel;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private BackgroundWorker worker;

        public LoadingWindow()
        {
            InitializeComponent();
            Start();
        }

        //this can be changed to load resources instead
        private void StartupWorker(object sender, DoWorkEventArgs e)
        {
            long sum = 0;
            long total = 50000;
            for (long i = 1; i <= total; i++)
            {
                sum += i;
                int percentage = Convert.ToInt32((double)i / total * 100);

                Dispatcher.Invoke(new Action(() =>
                {
                    worker.ReportProgress(percentage);
                }));
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MyProgressBar.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }

        private void Start()
        {
            MyProgressBar.Visibility = Visibility.Visible;
            worker = new BackgroundWorker();
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += StartupWorker;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
    }
}

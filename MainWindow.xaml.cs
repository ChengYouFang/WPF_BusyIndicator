
using System.Windows;
using System.Threading;
using System.ComponentModel;

namespace WpfApplication_Plugin
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += DoWorkEvent;
            bw.RunWorkerCompleted += RunWorkerCompletedEvent;
            bw.ProgressChanged += ProgressChanged;
            bw.RunWorkerAsync();
        }

        private void DoWorkEvent(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            for (int index = 1; index <= 10; index++)
            {
                if (!worker.CancellationPending)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(index * 10);
                }
                else
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void RunWorkerCompletedEvent(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("已取消了");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error");
            }
            else
            {
                busyIndicator.IsBusy = false;
                MessageBox.Show("Done");
            }
        }

    }
}

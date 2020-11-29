using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace WPF_Bitalino
{
    /// <summary>
    /// Interaction logic for Kalibrierung.xaml
    /// </summary>
    public partial class Kalibrierung : Window
    {
        private BackgroundWorker worker;
        //private System.Timers.Timer timer;
        private int round;
        //static System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();


        public Kalibrierung()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // set window position to middle of parent window
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }

        private void button_Abbruch_Click(object sender, RoutedEventArgs e)
        {
            if(worker!=null)
                worker.CancelAsync();

            this.Close();
        }

        private void button_Start_Click(object sender, RoutedEventArgs e)
        {            

            round = 0;
            label_Finger.Content = "kleiner Finger";

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;
            //timer = new System.Timers.Timer(100);
            // timer.Elapsed += timer_Elapsed;
            // timer.Start();
            worker.RunWorkerAsync();
        }


        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
         
            int i = 0;

            while (i < 5)
            {
                if (round == 5)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        label_Countdown.Content = "";
                    });                    
                }

                int number = 3;
                for (int counter = 3; counter >= 0; counter--)
                {
            
                    Thread.Sleep(1000);
                    int SoundDuration = 0;
                    int frequence = 0;

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                    // create fadingeffect
                    DoubleAnimation FadeInAnimation = new DoubleAnimation();
                        FadeInAnimation.From = 1;
                        FadeInAnimation.To = 0;
                        FadeInAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));


                        if (number == 0)
                        {
                            FadeInAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
                            label_Countdown.Content = "jetzt";
                            label_Countdown.BeginAnimation(Label.OpacityProperty, FadeInAnimation);
                            CountdownCircle.Fill = new SolidColorBrush(Color.FromArgb(255, 95, 176, 23));
                            frequence = 1400;
                            SoundDuration = 1000;
                        }
                        else
                        {
                            label_Countdown.Content = number.ToString();
                            label_Countdown.BeginAnimation(Label.OpacityProperty, FadeInAnimation);
                            SoundDuration = 800;
                            frequence = 1050;
                        }
                    });

                    // if Kalibration is cancled during operation
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        return;
                    }
                    System.Console.Beep(frequence, SoundDuration);
                    number--;
                }

                Thread.Sleep(900);
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    CountdownCircle.Fill = Brushes.Gray;
                });
                round++;
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //App.Current.Dispatcher.Invoke((Action)delegate
            //{
            //    //label_Countdown.Content = zahl.ToString();
            //    label_Countdown.Content = e.ProgressPercentage;
            //});
        }
    }

}

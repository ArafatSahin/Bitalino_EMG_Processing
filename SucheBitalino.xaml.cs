using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Bitalino
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SearchBluetoothWindow : Window
    {
        public string ID { get; set; }
        public string name { get; set; }

        public event EventHandler VerbindenClicked;    // Create event handler for receiving data in parent window


        private List<string> IDList = new List<string>(); // list for bitalino IDs
        private List<string> names = new List<string>();    // list for bitalino names
        private BackgroundWorker bw_scannbluetooth = new BackgroundWorker();

        public SearchBluetoothWindow()
        {
            InitializeComponent();
            
            // Bachgroundworker
            bw_scannbluetooth.DoWork += new DoWorkEventHandler(bw_scannbluetooth_DoWork);
            bw_scannbluetooth.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_scannbluetooth_RunWorkerCompleted);
            bw_scannbluetooth.WorkerReportsProgress = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // set window position to middle of parent window
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;

            //start backgroundworker
            bw_scannbluetooth.RunWorkerAsync();

            Duration duration = new Duration(TimeSpan.FromSeconds(13));
            System.Windows.Media.Animation.DoubleAnimation doubleanimation = new System.Windows.Media.Animation.DoubleAnimation(200.0, duration);
            Progressbar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        }

        // Backgroundworker events 
        private void bw_scannbluetooth_DoWork(object sender, DoWorkEventArgs e)
        {          
            // search for bluetooth devices
            Bitalino.DevInfo[] BTLdevice = Bitalino.find();

            // add all found bloutooth devices to combobox
            foreach (Bitalino.DevInfo d in BTLdevice)
            {
                IDList.Add(d.macAddr);
                names.Add(d.name);
            }
        }

        private void bw_scannbluetooth_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var item in IDList)
                comboBox_BitalinoIDs.Items.Add(item);

            comboBox_BitalinoIDs.IsDropDownOpen = true;
            Progressbar.Foreground = Brushes.LightGreen;
        }

        private void button_abbrechen_Click(object sender, RoutedEventArgs e)
        {
            Progressbar.Foreground = Brushes.Gray;            
            comboBox_BitalinoIDs.Items.Clear();
            this.Close();
        }

        private void button_verbinden_Click(object sender, RoutedEventArgs e)
        {
            if (VerbindenClicked != null)
            {
                name = names[comboBox_BitalinoIDs.SelectedIndex];
                ID = comboBox_BitalinoIDs.SelectedItem.ToString();
                Progressbar.Foreground = Brushes.Gray;
                comboBox_BitalinoIDs.Items.Clear();
                this.Close();
                VerbindenClicked(this, new EventArgs());
            }         
        }

        private void comboBox_BitalinoIDs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button_verbinden.IsEnabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPF_Bitalino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   
    public partial class MainWindow : Window
    {
        SearchBluetoothWindow SBW;
        Einstellung töne;

        public MainWindow()
        {
            InitializeComponent();
            LocationChanged += new EventHandler(Window_LocationChanged);          
        }
     
        
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            //
            foreach (Window win in this.OwnedWindows)
            {
                win.Left = this.Left + (this.Width - win.ActualWidth) / 2;
                win.Top = this.Top + (this.Height - win.ActualHeight) / 2;
            }
        }

        // when Bluetooth listbox is clicked
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SBW = new SearchBluetoothWindow();
            SBW.VerbindenClicked += new EventHandler(SearchBluetoothWindow_SubmitClicked);
            SBW.Owner = this;
            SBW.Show();
        }

        // this method gets fired when verbinden button of SBW window gets clicked
        private void SearchBluetoothWindow_SubmitClicked(object sender, EventArgs e)
        {
            try
            {                
                Bitalino dev = new Bitalino(SBW.ID);  // device MAC address   

                // set information labels on UI
                label_BitalinoName2.Content = SBW.name;
                label_BitalinoName2.Foreground = Brushes.LightGreen;
                label_BitalinoID2.Content = SBW.ID;
                label_BitalinoID2.Foreground = Brushes.LightGreen;

            }
            catch (Bitalino.Exception p)
            {
                MessageBox.Show("Verbindung fehlgeschlagen", "Achtung!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Kalibrieren_Item_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Kalibrierung KWindow = new Kalibrierung();
           // SBW.Owner = this;
            KWindow.ShowDialog();
        }
        
        
        
        private void button_startAquisition_Click(object sender, RoutedEventArgs e)
        {
            grid_Image.Visibility = Visibility.Visible;  
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}

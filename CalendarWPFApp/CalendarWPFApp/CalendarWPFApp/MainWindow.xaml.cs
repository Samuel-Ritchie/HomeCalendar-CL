using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calendar;
using Microsoft.Win32;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            main.Content = new SelectDB();
        }

        //method for opening Sam's window (window name not up to date)
        //private void openEventCreationPage_Click(object sender, RoutedEventArgs e)
        //{
        //    createPromptWindow secondWindow = new createPromptWindow();
        //    this.Visibility = Visibility.Hidden;
        //    secondWindow.Show();
        //}
    }
}
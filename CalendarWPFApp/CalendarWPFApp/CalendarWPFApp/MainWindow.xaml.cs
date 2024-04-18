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
        }

        private void fileExplorer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false; //cant select many files
            bool? choseFile = fileDialog.ShowDialog();



            if (choseFile == true)
            {
                //user picked a file
                string fullPath = fileDialog.FileName;
                string fileName = fileDialog.SafeFileName;
                chosenFileName.Text = fileName;
                chosenDirectoryName.Text = fullPath;

            }
            else
            {
                chosenFileName.Text = "Please select a valid file.";
            }
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {


            //validate selected file before finding?
            if (chosenFileName.Text == "Please select a valid file.")
            {
                ErrorFind.Text = "Please select a valid file before searching.";
            }
            else
            {
                //prompt next window

            }



        }

        private bool isSearch = false;
        private void swapBtnState_Click(object sender, RoutedEventArgs e)
        {
            if (!isSearch)
            {
                CreateFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
                TextBlock text = CreateFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

                SearchFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
                text = SearchFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

                isSearch = true;
            }
            else
            {
                CreateFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
                TextBlock text = CreateFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

                SearchFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
                text = SearchFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

                isSearch = false;
            }

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
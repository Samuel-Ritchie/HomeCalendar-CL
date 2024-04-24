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
using System.IO;
using Microsoft.Win32;
using PresenterInterfaceClasses;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        private Presenter _presenter;

        private bool _choiceIsValid = false;
        private bool _isSearchingFile = false;
        private bool _usingStandardTheme = true;
       
        public MainWindow()
        {
            InitializeComponent();

            _presenter = new Presenter(this);
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
                _choiceIsValid = true;
            }
            else
            {
                chosenFileName.Text = "Please select a valid file.";
                _choiceIsValid = false;
            }
        }
        private void swapBtnState_Click(object sender, RoutedEventArgs e)
        {
            ErrorFind.Text = "";
            if (!_isSearchingFile)
            {
                CreateFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
                TextBlock text = CreateFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

                SearchFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
                text = SearchFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

                _isSearchingFile = true;
            }
            else
            {
                CreateFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
                TextBlock text = CreateFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

                SearchFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
                text = SearchFileBtn.Child as TextBlock;
                text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

                _isSearchingFile = false;
            }
        }
        private void Header_Grab(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Swap_Theme_Click(object sender, RoutedEventArgs e)
        {
            _usingStandardTheme = (_usingStandardTheme) ? false : true;

            SolidColorBrush BG_VISIBLE = new BrushConverter().ConvertFrom("#665e9146") as SolidColorBrush;
            SolidColorBrush BG_HIDDEN = new BrushConverter().ConvertFrom("#222222") as SolidColorBrush;

            MainBG.Visibility = (_usingStandardTheme) ? Visibility.Visible : Visibility.Hidden;
            ColorBG.Background = (_usingStandardTheme) ? BG_VISIBLE : BG_HIDDEN;
        }
        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrorFind.Text = "";
            //validate selected file before finding?
            if (true)
            {
                // Pass ! _isSearchingFile so it is recieved as isNewDatabase. _isSearchingFile True == isNewDatabase False
                Window w = new CalendarWindow(_presenter);
                w.Show();
                this.Close();
            }
            else
            {
                ErrorFind.Text = "Please select a valid file before searching.";
            }
        }
        public void ShowMainError(string errMsg)
        {
            ErrorFind.Text = errMsg;
        }

        public void ShowCalendarInteractivity()
        {
            Window w = new CalendarWindow(_presenter);
            w.Show();
            this.Close();
        }

        public void ShowLocationPicker(string currentSaveLocation)
        {
            // Make user pick save location first.
            LocationPicker locationPickerWindow = new LocationPicker(this, _presenter, currentSaveLocation);
            locationPickerWindow.Show();
        }
    }
}
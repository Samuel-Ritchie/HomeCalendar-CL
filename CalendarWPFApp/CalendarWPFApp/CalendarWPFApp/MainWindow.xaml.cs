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

        //==============================================
        //  Event Handlers
        //==============================================
        private void fileExplorer_Click(object sender, RoutedEventArgs e)
        {
            if (_isSearchingFile)
            {
                // User is searching for existing database file.
                // Microsoft.Win32.OpenFileDialog

                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
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
            else
            {
                // User is selecting a path for creating a database file.
                // System.Windows.Forms.OpenFileDialog()

            }
        }
        private void swapBtnState_Click(object sender, RoutedEventArgs e)
        {
            // Change if user is creating new database or searching an existing one.
            ClearErrorMessage();

            if (!_isSearchingFile)
            {
                _isSearchingFile = true;
                SetUpPageForExistingDatabase();
            }
            else
            {
                _isSearchingFile = false;
                SetUpPageForNewDatabase();
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
            ClearErrorMessage();

            if (!_isSearchingFile)
            {
                // New Database.

            }
            else
            {
                // Search existing database.

                // Validate selected file before finding?
                if (_choiceIsValid)
                {
                    // Pass ! _isSearchingFile so it is recieved as isNewDatabase. _isSearchingFile True == isNewDatabase False
                    _presenter.ProcessDatabaseFile(chosenFileName.Text, chosenDirectoryName.Text, !_isSearchingFile);
                }
                else
                {
                    ErrorFind.Text = "Please select a valid file before searching.";
                }
            }

            
            
        }
        private void Header_Grab(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
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

        public void ClearErrorMessage()
        {
            ErrorFind.Text = "";
        }

        public void SetUpPageForNewDatabase()
        {
            /*
             * FindBtn: Change text to Create Calendar
             * HeaderForChooser: Change to "Choose a save location."
             * chosenDirectoryName: Set to existing value from presenter (default: documents)
             * DirectoryPathLabelBlock: Set text to "Directory path"
             */




            CreateFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
            TextBlock text = CreateFileBtn.Child as TextBlock;
            text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

            SearchFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
            text = SearchFileBtn.Child as TextBlock;
            text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

            _isSearchingFile = true;
        }

        public void SetUpPageForExistingDatabase()
        {
            /*
             * FindBtn: Change text to Open Calendar
             * HeaderForChooser: Change to "Choose a Calendar file."
             * chosenDirectoryName: Set to ""
             * DirectoryPathLabelBlock: Set text to "Full path"
             */

            CreateFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
            TextBlock text = CreateFileBtn.Child as TextBlock;
            text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

            SearchFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
            text = SearchFileBtn.Child as TextBlock;
            text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

            
        }
    }
}
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
using PresenterCode;
using System.IO;
using Microsoft.Win32;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ImainWindow
    {
        private presenter _presenter;
        private PromptCreateWindow _promptCreateWindow;
        private CreateEventWindow _createEventWindow;
        private CreateCategoryWindow _createCategoryWindow;

        private string _filePath;

        public MainWindow()
        {
            InitializeComponent();

            _promptCreateWindow = new PromptCreateWindow();
            _createEventWindow = new CreateEventWindow();
            _createCategoryWindow = new CreateCategoryWindow();

            _presenter = new presenter(this, _promptCreateWindow, _createEventWindow, _createCategoryWindow);

            _filePath = "";
        }

        private void fileExplorer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false; //cant select many files
            bool? choseFile = fileDialog.ShowDialog();

            if(choseFile == true)
            {
                //user picked a file
                _filePath = fileDialog.FileName;

                // Change view
                string fullPath = fileDialog.FileName;
                string fileName = fileDialog.SafeFileName;
                chosenFileName.Text = fileName;
                chosenDirectoryName.Text = fullPath;
            } else
            {
                //did not pick a file
            }
        }

        private void openEventCreationPage_Click(object sender, RoutedEventArgs e)
        {
            // If database file does not exist, pop out dsclaimer window that asks user if they want to create a new database file.
            if (_filePath != null && File.Exists(_filePath))
            {
                this.Visibility = Visibility.Hidden;
                _promptCreateWindow.Show();
            }
            else if (_filePath != null)
            {
                // display file not chosen
            }
            else if (!File.Exists(_filePath))
            {
                // Display file does not exist.
                // Pop up Disclaimer window. Ask user if they want to create new database file with that name.
                // If not, close pop up and stay on file picker window.


            }
        }
    }
}
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
        private bool _openExistentDatabase;

        public MainWindow()
        {
            InitializeComponent();

            _promptCreateWindow = new PromptCreateWindow(this);
            _createEventWindow = new CreateEventWindow(this);
            _createCategoryWindow = new CreateCategoryWindow(this);

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
                _filePath = fullPath;

            } else
            {
                chosenFileName.Text = "Please select a valid db file.";
            }
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {


            //validate selected file before finding?
            if (chosenFileName.Text == "Please select a valid file.")
            {
                ErrorFind.Text = "Please select a valid file before searching.";
            }else
            {
                // File name is valid.
                if (FindBtn.Content.ToString() == "Create calendar")
                {
                    // Initialize HomeCalendar in model with existing file.
                    _presenter.InitializeHomeCalendar(_filePath, true);
                }
                else if (FindBtn.Content.ToString() == "Find calendar")
                {
                    // Initialize HomeCalendar in model with existing file.
                    _presenter.InitializeHomeCalendar(_filePath , false);
                }
            }
        }

        private void swapBtnState_Click(object sender, RoutedEventArgs e)
        {
            const string CREATE_CALENDAR_STRING = "Create calendar";
            const string FIND_CALENDAR_STRING = "Find calendar";
       
                if (FindBtn.Content.ToString() == "Find" || FindBtn.Content.ToString() == FIND_CALENDAR_STRING)
                {
                    FindBtn.Content = CREATE_CALENDAR_STRING;

                }
                else if (FindBtn.Content.ToString() == CREATE_CALENDAR_STRING)
                {
                    FindBtn.Content = FIND_CALENDAR_STRING;
                }
        }

        //method for opening Sam's window
        private void openEventCreationPage_Click(object sender, RoutedEventArgs e)
        {


            this.Visibility = Visibility.Hidden;
            _promptCreateWindow.Show();
        }

        //==============================================
        //  Interface Methods
        //==============================================

        public void OpenPromptCreateWindow()
        {
            this.Visibility = Visibility.Hidden;
            _promptCreateWindow.Show();
        }
    }
}
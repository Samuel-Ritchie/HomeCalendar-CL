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
        private string _fileName;
        private bool _databaseAlreadyExists;

        public MainWindow()
        {
            InitializeComponent();

            /*
            _promptCreateWindow = new PromptCreateWindow(this);
            _createEventWindow = new CreateEventWindow(this);
            _createCategoryWindow = new CreateCategoryWindow(this);
            */

            _presenter = new presenter(this/*, _promptCreateWindow, _createEventWindow, _createCategoryWindow*/);

            _filePath = "";
            _fileName = "";
        }

        //==============================================
        //  Event Handlers
        //==============================================

        private void fileExplorer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false; //cant select many files
            bool? choseFile = fileDialog.ShowDialog();

            if(choseFile == true)
            {
                //user picked a file
                _filePath = fileDialog.FileName;
                _fileName = fileDialog.FileName;

                // Change view
                ChangeDatabaseNameAndPathDisplay(_fileName, _filePath);

            } else
            {
                ShowError("Please select a valid db file.");
            }
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {


            //validate selected file before finding?
            if (chosenFileName.Text == "Please select a valid file.")
            {
                ShowError("Please select a valid file before searching.");
            }else
            {
                // File name is valid.
                if (FindBtn.Content.ToString() == "Create calendar")
                {
                    // Initialize HomeCalendar in model with existing file.
                    _databaseAlreadyExists = true;
                    _presenter.InitializeHomeCalendar(_fileName, _filePath, _databaseAlreadyExists);
                }
                else if (FindBtn.Content.ToString() == "Find calendar")
                {
                    // Initialize HomeCalendar in model with existing file.
                    _databaseAlreadyExists = false;
                    _presenter.InitializeHomeCalendar(_fileName, _filePath , _databaseAlreadyExists);
                }
            }
        }

        private void swapBtnState_Click(object sender, RoutedEventArgs e)
        {
            const string CREATE_CALENDAR_STRING = "Create calendar";
            const string OPEN_CALENDAR_STRING = "Open calendar";
       
                if (FindBtn.Content.ToString() == OPEN_CALENDAR_STRING)
                {
                    FindBtn.Content = CREATE_CALENDAR_STRING;

                }
                else if (FindBtn.Content.ToString() == CREATE_CALENDAR_STRING)
                {
                    FindBtn.Content = OPEN_CALENDAR_STRING;
                }
        }

        //==============================================
        //  Interface Methods
        //==============================================
        public void OpenPromptCreateWindow()
        {
            this.Visibility = Visibility.Hidden;
            _promptCreateWindow.Show();
        }

        public void ShowError(string errorMsg)
        {
            ErrorFind.Text = errorMsg;
        }

        //==============================================
        //  Specific Methods
        //==============================================
        public void ChangeDatabaseNameAndPathDisplay(string name, string path)
        {

            chosenFileName.Text = name;
            chosenDirectoryName.Text = path;
        }
    }
}
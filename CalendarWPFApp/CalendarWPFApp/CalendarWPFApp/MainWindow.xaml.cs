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
using PresenterInterfaceClasses;
using Microsoft.Win32;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView, IPromptCreationWindow, IEventForm, ICategoryForm
    {
        private presenter _presenter;

        private string _filePath;
        private string _fileName;
        private bool _databaseAlreadyExists;

        public MainWindow()
        {
            InitializeComponent();
            
            _presenter = new presenter(this);

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
                ShowMainError("Please select a valid db file.");
            }
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {


            //validate selected file before finding?
            if (chosenFileName.Text == "Please select a valid file.")
            {
                ShowMainError("Please select a valid file before searching.");
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
        //  Specific Methods
        //==============================================
        public void ChangeDatabaseNameAndPathDisplay(string name, string path)
        {

            chosenFileName.Text = name;
            chosenDirectoryName.Text = path;
        }

        //==============================================
        //  Interface Methods
        //==============================================

        // Main View Methods
        //2
        void ShowMainError(string errMsg)
        {
            throw new NotImplementedException();
        }

        void ShowCalendarInteractivity()
        {
            throw new NotImplementedException();
        }

        // PromptCreationWindow Interface methods
        // 3
        public void ShowCreateEventForm()
        {
            throw new NotImplementedException();
        }

        public void ShowCreateCategoryForm()
        {
            throw new NotImplementedException();
        }

        public void DoubleCheckCloseApplication()
        {
            throw new NotImplementedException();
        }

        // Event form interface methods.
        // 5
        public void DoubleCheckCloseEventForm()
        {
            throw new NotImplementedException();
        }

        public void ClearEventForm()
        {
            throw new NotImplementedException();
        }

        public void ShowEventCreated()
        {
            throw new NotImplementedException();
        }

        public void ShowEventCreationError()
        {
            throw new NotImplementedException();
        }

        // Category form interface methods.
        // 6
        public void ShowCreateCategoryFormFromEventForm()
        {
            throw new NotImplementedException();
        }

        public void DoubleCheckCloseCategoryForm()
        {
            throw new NotImplementedException();
        }

        public void ClearCategoryForm()
        {
            throw new NotImplementedException();
        }

        public void ShowCategoryCreated()
        {
            throw new NotImplementedException();
        }

        public void ShowCategoryCreationError()
        {
            throw new NotImplementedException();
        }

        public void ReturnToEventForm()
        {
            throw new NotImplementedException();
        }

    }
}
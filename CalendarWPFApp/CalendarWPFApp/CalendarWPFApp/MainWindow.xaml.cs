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
                chosenFileName.Text = "Please select a valid file.";
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
                //prompt next window
                
            }

            

        }

        private void swapBtnState_Click(object sender, RoutedEventArgs e)
        {
            string createCalendar = "Create calendar";
            string findCalendar = "Find calendar";
       
                if (FindBtn.Content.ToString() == "Find" || FindBtn.Content.ToString() == findCalendar)
                {
                    FindBtn.Content = createCalendar;

                }
                else if (FindBtn.Content.ToString() == createCalendar)
                {
                    FindBtn.Content = findCalendar;
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
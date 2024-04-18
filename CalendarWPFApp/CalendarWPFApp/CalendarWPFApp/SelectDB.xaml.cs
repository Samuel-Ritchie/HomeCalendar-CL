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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for SelectDB.xaml
    /// </summary>
    public partial class SelectDB : Page
    {
        private string _filePath;
        private string _fileName;
        private bool _isSearchingFile;

        public SelectDB()
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
        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {

            //validate selected file before finding?
            if (chosenFileName.Text == "Please select a valid file.")
            {
                //ErrorFind.Text = "Please select a valid file before searching.";
            }
            else
            {
                //prompt next window

            }
        }

        ////==============================================
        ////  Event Handlers
        ////==============================================

        //private void fileExplorer_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    fileDialog.Multiselect = false; //cant select many files
        //    bool? choseFile = fileDialog.ShowDialog();

        //    if (choseFile == true)
        //    {
        //        //user picked a file
        //        _filePath = fileDialog.FileName;
        //        _fileName = fileDialog.FileName;

        //        // Change view
        //        ChangeDatabaseNameAndPathDisplay(_fileName, _filePath);

        //    }
        //    else
        //    {
        //        ShowError("Please select a valid db file.");
        //    }
        //}

        //private void OpenWindow_Click(object sender, RoutedEventArgs e)
        //{


        //    //validate selected file before finding?
        //    if (chosenFileName.Text == "Please select a valid file.")
        //    {
        //        ShowError("Please select a valid file before searching.");
        //    }
        //    else
        //    {
        //        // File name is valid.
        //        if (FindBtn.Content.ToString() == "Create calendar")
        //        {
        //            // Initialize HomeCalendar in model with existing file.
        //            _isSearchingFile = true;
        //            //_presenter.InitializeHomeCalendar(_fileName, _filePath, _isSearchingFile);
        //        }
        //        else if (FindBtn.Content.ToString() == "Find calendar")
        //        {
        //            // Initialize HomeCalendar in model with existing file.
        //            _isSearchingFile = false;
        //            //_presenter.InitializeHomeCalendar(_fileName, _filePath, _databaseAlreadyExists);
        //        }
        //    }
        //}

        ////==============================================
        ////  Interface Methods
        ////==============================================
        //public void OpenPromptCreateWindow()
        //{
        //    //switch frame
        //}

        //public void ShowError(string errorMsg)
        //{
        //    ErrorFind.Text = errorMsg;
        //}

        ////==============================================
        ////  Specific Methods
        ////==============================================
        //public void ChangeDatabaseNameAndPathDisplay(string name, string path)
        //{

        //    chosenFileName.Text = name;
        //    chosenDirectoryName.Text = path;
        //}
        //private void swapBtnState_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!_isSearchingFile)
        //    {
        //        CreateFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
        //        TextBlock text = CreateFileBtn.Child as TextBlock;
        //        text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

        //        SearchFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
        //        text = SearchFileBtn.Child as TextBlock;
        //        text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

        //        _isSearchingFile = true;
        //    }
        //    else
        //    {
        //        CreateFileBtn.Background = new BrushConverter().ConvertFrom("#5e9146") as SolidColorBrush;
        //        TextBlock text = CreateFileBtn.Child as TextBlock;
        //        text.Foreground = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

        //        SearchFileBtn.Background = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
        //        text = SearchFileBtn.Child as TextBlock;
        //        text.Foreground = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;

        //        _isSearchingFile = false;
        //    }

        //}

        //private void FindBtn_Click(object sender, RoutedEventArgs e)
        //{

        //    //validate selected file before finding?
        //    if (chosenFileName.Text == "Please select a valid file.")
        //    {
        //        //ErrorFind.Text = "Please select a valid file before searching.";
        //    }
        //    else
        //    {
        //        //prompt next window

        //    }
        //}
    }
}

using PresenterCode;
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
using System.Windows.Shapes;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for PromptCreateWindow.xaml
    /// </summary>
    public partial class PromptCreateWindow : Window, IpromptCreateWindow
    {
        // Main window reference.
        private ImainWindow _mainWindow;

        public PromptCreateWindow(ImainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        //==============================================
        //  Event Handlers
        //==============================================
        private void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //==============================================
        //  Interface Methods
        //==============================================
        public void ChangeDisplayInfo(string databaseName)
        {
            DatabaseNameDisplay.Text = databaseName;
        }
    }
}

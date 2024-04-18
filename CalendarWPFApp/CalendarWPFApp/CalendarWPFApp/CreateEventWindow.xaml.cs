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
    /// Interaction logic for CreateEventWindow.xaml
    /// </summary>
    public partial class CreateEventWindow : Window, IcreateEventWindow
    {
        // Main window reference.
        private ImainWindow _mainWindow;

        public CreateEventWindow(ImainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }
    }
}

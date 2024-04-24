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
using Calendar;

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            CalendarItemsTable.ItemsSource = GetTestData();
        }
        private List<Event> GetTestData()
        {
            return new List<Event>()
            {
                new Event(DateTime.Now, 1, 3.0, "Wah"),
                new Event(DateTime.Now, 3, 6.0, "TEST"),
                new Event(DateTime.Now, 3, 6.0, "Real")
            };
        }
    }
}

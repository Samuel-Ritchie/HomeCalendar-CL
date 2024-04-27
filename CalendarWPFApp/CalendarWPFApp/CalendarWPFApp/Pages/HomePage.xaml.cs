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

        private void GetFilteringCriteria()
        {
            //getting selected dates
            DateTime? startDate = startDateChosen.SelectedDate;
            DateTime? endDate = endDateChosen.SelectedDate;

            //filter by a category
            bool? isFilterByCategoryChecked = filterCategoryCheckbox.IsChecked;

            int selectedCategoryId = -1; //placeholder
            //parse from filterCategory.SelectedValue (object) --> string --> int
            bool parseResult = int.TryParse(filterCategory.SelectedValue.ToString(), out selectedCategoryId);


            //call specific byMonth byCategory .. or both ?
            bool? isByMonthChecked = byMonthCheck.IsChecked;
            bool? isByCategoryChecked = byCategoryCheck.IsChecked;

            SortEvents(startDate, endDate, isFilterByCategoryChecked, selectedCategoryId);
        }

        private void SortEvents(DateTime? startDate, DateTime? endDate, bool? FilterFlag, int CategoryID)
        {

        }
    }
}

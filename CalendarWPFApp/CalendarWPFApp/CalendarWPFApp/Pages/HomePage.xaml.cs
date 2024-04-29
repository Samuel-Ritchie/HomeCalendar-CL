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
        //for testing
        HomeCalendar _calendar;
        public HomePage()
        {
            InitializeComponent();
            _calendar = new HomeCalendar("../../testDbInput");

            SetDataGridCalendarItems();
        }

        //for testing
        private List<CalendarItem> GetTestData()
        {

            return new List<CalendarItem>()
            {
                new CalendarItem()
                {
                    CategoryID = 1,
                    EventID = 2,
                    StartDateTime = DateTime.Now,
                    Category = _calendar.categories.GetCategoryFromId(1).Description,
                    ShortDescription = "Wah",
                    DurationInMinutes = 1580,
                    BusyTime = 23
                },
                new CalendarItem()
                {
                    CategoryID = 2,
                    EventID = 2,
                    StartDateTime = DateTime.Now,
                    Category = _calendar.categories.GetCategoryFromId(2).Description,
                    ShortDescription = "TEST",
                    DurationInMinutes = 1580,
                    BusyTime = 23
                },
                new CalendarItem()
                {
                    CategoryID = 3,
                    EventID = 2,
                    StartDateTime = DateTime.Now,
                    Category = _calendar.categories.GetCategoryFromId(3).Description,
                    ShortDescription = "Real",
                    DurationInMinutes = 1580,
                    BusyTime = 23
                }
            };
        }
        private void SetDataGridCalendarItems()
        {
            List<CalendarItem> data = GetTestData();

            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            DataGridTextColumn column = new DataGridTextColumn();

            column.Header = "Start Date";
            column.Binding = new Binding("StartDateTime");
            column.Binding.StringFormat = "yyyy/MM/dd";
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Start Time";
            column.Binding = new Binding("StartDateTime");
            column.Binding.StringFormat = "HH:mm tt";
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Category";
            column.Binding = new Binding("Category");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Description";
            column.Binding = new Binding("ShortDescription");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Duration";
            column.Binding = new Binding("DurationInMinutes");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Busy Time";
            column.Binding = new Binding("BusyTime");
            CalendarItemsTable.Columns.Add(column);
        }
    }
}

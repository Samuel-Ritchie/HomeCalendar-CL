using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Calendar;
using PresenterInterfaceClasses;

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page, IHomePage
    {
        private Presenter _presenter;
        private IHomePage _homePageInterface;

        public HomePage(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;

            SetDataGridCalendarItems();
        }
        private void SetDataGridCalendarItems()
        {
            List<CalendarItem> data = _presenter.SortEvents(null, null, false, 1);

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


        //methods for updating the datagrid resource
        public void UpdateEventsGetCalendarItems(List<Calendar.CalendarItem> calendarItems)
        {
            CalendarItemsTable.ItemsSource = calendarItems;
        }
        public void UpdateEventsGetCalendarItemsByCategory(List<Calendar.CalendarItemsByCategory> calendarItemsByCategory)
        {
            CalendarItemsTable.ItemsSource = calendarItemsByCategory;
        }
        public void UpdateEventsGetCalendarItemsByMonth(List<Calendar.CalendarItemsByMonth> calendarItemsByMonth)
        {
            CalendarItemsTable.ItemsSource = calendarItemsByMonth;
        }
        public void UpdateEventsGetCalendarItemsByCategoryAndMonth(List<Dictionary<string, object>> calendarItemsByCatAndMonth)
        {
            CalendarItemsTable.ItemsSource = calendarItemsByCatAndMonth;
        }


        //essentially this should be called anytime any UI element gets a new selection
        //only the first checkbox has this method in it
        //TODO: Make all the other elements (startdate, end, bymonth, bycategory) funnel to this method
        private void getUserInput_Click(object sender, RoutedEventArgs e)
        {
            //getting selected dates, if null set to start =  yesterday, end = today
            DateTime? startDate = startDateChosen.SelectedDate == null ? DateTime.Today.AddDays(-1) : startDateChosen.SelectedDate;
            DateTime? endDate = endDateChosen.SelectedDate == null ? DateTime.Today : endDateChosen.SelectedDate;

            //filter by a category
            bool isFilterByCategoryChecked = filterCategoryCheckbox.IsChecked == true;

            int selectedCategoryId = -1; //placeholder
            //parse from filterCategory.SelectedValue (object) --> string --> int
            if (filterCategory.SelectedValue != null)
            {
                 bool parseResult = int.TryParse(filterCategory.SelectedValue.ToString(), out selectedCategoryId);
            } else
            {
                isFilterByCategoryChecked = false;
            }

            bool? isMonthChecked = byMonthCheck.IsChecked;
            bool? isCategoryChecked = byCategoryCheck.IsChecked;

            //update the sorted list of events each time there is a trigger
            _presenter.SortEvents(null, null, false, 1);
        }
    }
}

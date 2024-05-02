using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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

        public HomePage(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;

            SetCalendarItems();
            SetCategories();
        }

        private void SetCategories()
        {
            filterCategory.ItemsSource = _presenter.GetListOfCategories();
            filterCategory.DisplayMemberPath = "Description";
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
            DateTime? startDate = startDateChosen.SelectedDate == null ? new DateTime(1900, 1, 1) : startDateChosen.SelectedDate;
            DateTime? endDate = endDateChosen.SelectedDate == null ? new DateTime(2500, 1, 1) : endDateChosen.SelectedDate;

            //filter by a category
            bool isFilterByCategoryChecked = filterCategoryCheckbox.IsChecked == true;

            int selectedCategoryId = 0; //placeholder
            //parse from filterCategory.SelectedValue (object) --> string --> int
            if (filterCategory.SelectedValue != null)
            {
                Category c = filterCategory.SelectedItem as Category;
                selectedCategoryId = c.Id;
            } else
            {
                isFilterByCategoryChecked = false;
            }

            bool isMonthChecked = (byMonthCheck.IsChecked is not null && byMonthCheck.IsChecked == true) ? true : false;
            bool isCategoryChecked = (byCategoryCheck.IsChecked is not null && byCategoryCheck.IsChecked == true) ? true : false;

            if (!isMonthChecked && !isCategoryChecked)
                SetCalendarItems(startDate, endDate, isFilterByCategoryChecked, selectedCategoryId);
            else if (isMonthChecked && !isCategoryChecked)
                SetCalendarItemsByMonth(startDate, endDate, isFilterByCategoryChecked, selectedCategoryId);
            else if (!isMonthChecked && isCategoryChecked)
                SetCalendarItemsByCategory(startDate, endDate, isFilterByCategoryChecked, selectedCategoryId); 
            else
                SetCalendarItemsByCategoryAndMonth(startDate, endDate, isFilterByCategoryChecked, selectedCategoryId);
        }

        private void SetCalendarItems(DateTime? start = null, DateTime? end = null, bool filter = false, int category = 0)
        {
            List<CalendarItem> data = _presenter.SortEvents(start, end, filter, category);

            CalendarItemsTable.HeadersVisibility = DataGridHeadersVisibility.Column;
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            DataGridTextColumn column = new DataGridTextColumn();

            column.Header = "Start Date";
            column.Binding = new System.Windows.Data.Binding("StartDateTime");
            column.Binding.StringFormat = "yyyy/MM/dd";
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Start Time";
            column.Binding = new System.Windows.Data.Binding("StartDateTime");
            column.Binding.StringFormat = "HH:mm tt";
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Category";
            column.Binding = new System.Windows.Data.Binding("Category");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Description";
            column.Binding = new System.Windows.Data.Binding("ShortDescription");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Duration";
            column.Binding = new System.Windows.Data.Binding("DurationInMinutes");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Busy Time";
            column.Binding = new System.Windows.Data.Binding("BusyTime");
            CalendarItemsTable.Columns.Add(column);
        }
        private void SetCalendarItemsByMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int category = 0)
        {
            List<CalendarItemsByMonth> data = _presenter.SortEventsByMonth(start, end, filter, category);

            CalendarItemsTable.HeadersVisibility = DataGridHeadersVisibility.Column;
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            DataGridTextColumn column = new DataGridTextColumn();

            column = new DataGridTextColumn();
            column.Header = "Month";
            column.Binding = new System.Windows.Data.Binding("Month");
            CalendarItemsTable.Columns.Add(column);


            column = new DataGridTextColumn();
            column.Header = "Total Busy Time";
            column.Binding = new System.Windows.Data.Binding("TotalBusyTime");
            CalendarItemsTable.Columns.Add(column);
        }
        private void SetCalendarItemsByCategory(DateTime? start = null, DateTime? end = null, bool filter = false, int category = 0)
        {
            List<CalendarItemsByCategory> data = _presenter.SortEventsByCategory(start, end, filter, category);

            CalendarItemsTable.HeadersVisibility = DataGridHeadersVisibility.Column;
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            DataGridTextColumn column = new DataGridTextColumn();

            column = new DataGridTextColumn();
            column.Header = "Category";
            column.Binding = new System.Windows.Data.Binding("Category");
            CalendarItemsTable.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Total Busy Time";
            column.Binding = new System.Windows.Data.Binding("TotalBusyTime");
            CalendarItemsTable.Columns.Add(column);
        }
        private void SetCalendarItemsByCategoryAndMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int category = 0)
        {
            List<Dictionary<string, object>> data = _presenter.SortEventsByCategoryAndMonth(start, end, filter, category);

            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            DataGridTextColumn column = new DataGridTextColumn();

            for (int categoryK = 0; category < 1; category++)
            {
                foreach (var a in data[categoryK].Keys)
                {
                    column = new DataGridTextColumn();
                    column.Header = a;
                    column.Binding = new System.Windows.Data.Binding($"[{a}]");
                    CalendarItemsTable.Columns.Add(column);
                }
            }
        }
    }
}

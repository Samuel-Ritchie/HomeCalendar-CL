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

        //essentially this should be called anytime any UI element gets a new selection
        private void getUserInput_Click(object sender, RoutedEventArgs e)
        {
            //getting selected dates, if null set to start =  yesterday, end = today
            DateTime startDate = (startDateChosen.SelectedDate == null) ? new DateTime(1900, 1, 1) : (DateTime)startDateChosen.SelectedDate;
            DateTime endDate = (endDateChosen.SelectedDate == null) ? new DateTime(2500, 1, 1) : (DateTime)endDateChosen.SelectedDate;

            //filter by a category
            bool isFilterByCategoryChecked = filterCategoryCheckbox.IsChecked == true;

            int selectedCategoryId = 0; 

            if (filterCategory.SelectedValue != null)
            {
                Category category = filterCategory.SelectedItem as Category;
                selectedCategoryId = category.Id;
            }
            else
                isFilterByCategoryChecked = false;

            bool isMonthChecked = (byMonthCheck.IsChecked is not null && byMonthCheck.IsChecked == true) ? true : false;
            bool isCategoryChecked = (byCategoryCheck.IsChecked is not null && byCategoryCheck.IsChecked == true) ? true : false;

            _presenter.DisplayCalendarItems(this, isMonthChecked, isCategoryChecked, startDate, endDate, isFilterByCategoryChecked, selectedCategoryId);
        }
        private void SetColumn(string header, string binding, string format="")
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = header;
            column.Binding = new System.Windows.Data.Binding(binding);
            column.Binding.StringFormat = format;
            CalendarItemsTable.Columns.Add(column);
        }

        // =====================================
        //  Interface functions
        // =====================================
        public void SetCalendarItems(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            List<CalendarItem> data = _presenter.SortEvents(start, end, filter, searchCategory);
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            SetColumn("Start Date", "StartDateTime", "yyyy/MM/dd");
            SetColumn("Start Time", "StartDateTime", "HH:mm tt");
            SetColumn("Category", "Category");
            SetColumn("Description", "ShortDescription");
            SetColumn("Duration", "DurationInMinutes", "N2");
            SetColumn("Busy Time", "BusyTime", "N2");
        }
        public void SetCalendarItemsByMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            List<CalendarItemsByMonth> data = _presenter.SortEventsByMonth(start, end, filter, searchCategory);
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            SetColumn("Month", "Month");
            SetColumn("Total Busy Time", "TotalBusyTime", "N2");
        }
        public void SetCalendarItemsByCategory(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            List<CalendarItemsByCategory> data = _presenter.SortEventsByCategory(start, end, filter, searchCategory);
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            SetColumn("Category", "Category");
            SetColumn("Total Busy Time", "TotalBusyTime", "N2");
        }
        public void SetCalendarItemsByCategoryAndMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            List<Dictionary<string, object>> data = _presenter.SortEventsByCategoryAndMonth(start, end, filter, searchCategory);
            CalendarItemsTable.ItemsSource = data;
            CalendarItemsTable.Columns.Clear();

            SetColumn("Category", "[Month]");

            List<Category> categories = _presenter.GetListOfCategories();

            for (int category = 0; category < categories.Count; category++)
                SetColumn(categories[category].Description, $"[{categories[category].Description}]", "N2");

            SetColumn("Total Busy Time", "[TotalBusyTime]", "N2");
        }
    }
}

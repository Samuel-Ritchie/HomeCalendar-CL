﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calendar;
using PresenterInterfaceClasses;

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private Presenter _presenter;

        public HomePage(Presenter p)
        {
            InitializeComponent();
            _presenter = p;

            //CalendarItemsTable.ItemsSource = GetTestData();

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

        public void GetFilteringCriteria()
        {
            //getting selected dates
            DateTime? startDate = startDateChosen.SelectedDate;
            DateTime? endDate = endDateChosen.SelectedDate;

            //filter by a category
            bool? isFilterByCategoryChecked = filterCategoryCheckbox.IsChecked;

            int selectedCategoryId = -1; //placeholder
            //parse from filterCategory.SelectedValue (object) --> string --> int
            bool parseResult = int.TryParse(filterCategory.SelectedValue.ToString(), out selectedCategoryId);


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
        private void filterCategoryCheckbox_Click(object sender, RoutedEventArgs e)
        {
            //getting selected dates
            DateTime? startDate = startDateChosen.SelectedDate;
            DateTime? endDate = endDateChosen.SelectedDate;

            //filter by a category
            bool isFilterByCategoryChecked = filterCategoryCheckbox.IsChecked == true;

            int selectedCategoryId = -1; //placeholder
            //parse from filterCategory.SelectedValue (object) --> string --> int
            bool parseResult = int.TryParse(filterCategory.SelectedValue.ToString(), out selectedCategoryId);

            bool? isMonthChecked = byMonthCheck.IsChecked;
            bool? isCategoryChecked = byCategoryCheck.IsChecked;

            //update the sorted list of events each time there is a trigger
            _presenter.SortEvents(startDate, endDate, isFilterByCategoryChecked, selectedCategoryId, isMonthChecked, isCategoryChecked);
        }
    }
}

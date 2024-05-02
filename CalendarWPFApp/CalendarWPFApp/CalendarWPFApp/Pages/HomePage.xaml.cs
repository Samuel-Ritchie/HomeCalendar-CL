﻿using System;
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
using PresenterInterfaceClasses;

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        Presenter _presenter;
        public HomePage(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;

            SetDataGridCalendarItems();
        }
        private void SetDataGridCalendarItems()
        {
            List<CalendarItem> data = null; 

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

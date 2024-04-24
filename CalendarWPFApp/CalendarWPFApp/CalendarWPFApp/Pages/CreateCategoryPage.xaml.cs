using PresenterInterfaceClasses;
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

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for CreateCategoryPage.xaml
    /// </summary>
    public partial class CreateCategoryPage : Page, ICategoryForm
    {
        // Calendar window reference.
        CalendarWindow _calendarWindow;
        CreateEventPage? _eventWindow;

        Presenter _presenter;

        private bool _initializedFromCalendarWindow;

        public CreateCategoryPage(Presenter presenter, CalendarWindow calendarWindow, CreateEventPage? eventWindow)
        {
            InitializeComponent();

            _presenter = presenter;

            _initializedFromCalendarWindow = true;
            _calendarWindow = calendarWindow;

            if (eventWindow is not null)
            {
                _initializedFromCalendarWindow = false;
                _eventWindow = eventWindow;
            }
        }

        //==============================================
        //  Event Handlers
        //==============================================

        private void doneDescBtn(object sender, RoutedEventArgs e)
        {
            /*
             * CategoryTypeComboBox
             * DescriptionBox
             */

            if (CategoryTypeComboBox.SelectedValue is not null)
            {
                string categoryType = ((ComboBoxItem)CategoryTypeComboBox.SelectedValue).Content.ToString();

                if (DescriptionBox.Text != "")
                {
                    _presenter.processCategoryForm(this, categoryType, DescriptionBox.Text);
                }
                else
                {
                    ShowCategoryCreationError("Description not given.");
                }
            }
            else
            {
                ShowCategoryCreationError("Category type not chosen.");
            }
            //category = Id, Description, CategoryType

        }

        //==============================================
        //  Interface methods
        //==============================================

        public void ClearCategoryForm()
        {
            throw new NotImplementedException();
        }

        public void DoubleCheckCloseCategoryForm()
        {
            throw new NotImplementedException();
        }

        public void ReturnToEventForm()
        {
            throw new NotImplementedException();
        }

        public void ShowCategoryCreated()
        {
            MessageBoxResult userChoice = MessageBox.Show("Category has been created.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            if (_initializedFromCalendarWindow)
            {
                _calendarWindow.SwitchForms(CalendarWindow.Interfaces.CalendarView, null);
            }
            else if (!_initializedFromCalendarWindow)
            {
                _calendarWindow.SwitchForms(CalendarWindow.Interfaces.CreateEvent, _eventWindow);
            }
        }

        public void ShowCategoryCreationError(string errMessage)
        {
            // Display Error to do with Category Creation to user using Message Box.
            MessageBoxResult userChoice = MessageBox.Show(errMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

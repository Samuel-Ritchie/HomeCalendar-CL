using System.Windows;
using System.Windows.Controls;
using PresenterInterfaceClasses;

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for CreateEventPage.xaml
    /// </summary>
    public partial class CreateEventPage : Page, IEventForm
    {
        CalendarWindow _calendarWindow;
        Presenter _presenter;

        public CreateEventPage(Presenter presenter, CalendarWindow CalendarWindow)
        {
            InitializeComponent();

            _presenter = presenter;
            _calendarWindow = CalendarWindow;

            HoursComboBox.ItemsSource = GetIntRange(1, 12);
            MinutesComboBox.ItemsSource = GetIntRange(0, 59);
        }

        private List<string> GetIntRange(int start, int end)
        {
            List<string> items = new List<string>();

            for (int i = start; i <= end; i++)
                items.Add(i.ToString());

            return items;
        }

        //==============================================
        //  Event Handlers
        //==============================================
        private void Categories_DropDownOpened(object sender, EventArgs e)
        {
            _presenter.GetListOfCategories(this);
        }

        private void CancelTheEvent_Click(object sender, RoutedEventArgs e)
        {
            _calendarWindow.SwitchForms(CalendarWindow.Interfaces.ChooseToCreate, null);
        }

        private void CreateTheEvent_Click(object sender, RoutedEventArgs e)
        {
            /*
           DetailsBox
           datePickerInput
           HoursComboBox
           MinutesComboBox
           SecondsComboBox
           AMPMComboBox
           DurationBox
           Categories
           */

            string theDetails;
            DateTime theDateTime;
            int theHours;
            int theMinutes;
            string theAmPm;
            int theDurationInMinutes;
            string category;

            if (DetailsBox.Text != "")
            {
                theDetails = (string)DetailsBox.Text;
                if (datePickerInput.SelectedDate is not null)
                {
                    theDateTime = (DateTime)datePickerInput.SelectedDate;
                    if (HoursComboBox.SelectedValue is not null && int.TryParse(HoursComboBox.SelectedValue.ToString(), out theHours))
                    {
                        if (AMPMComboBox.SelectedValue is not null && AMPMComboBox.SelectedValue.ToString() == "PM")
                        {
                            theHours = theHours + 12;
                        }
                        if (MinutesComboBox.SelectedValue is not null && int.TryParse(MinutesComboBox.SelectedValue.ToString(), out theMinutes))
                        {
                            int duration;
                            if (DurationBox.Text.ToString() != "" &&
                                int.TryParse(DurationBox.Text.ToString(), out theDurationInMinutes))
                            {
                                if ((ComboBoxItem)Categories.SelectedValue is not null)
                                {
                                    category = ((ComboBoxItem)Categories.SelectedValue).Content.ToString();

                                    _presenter.processEventForm(
                                        this,
                                        theDetails,
                                        theDateTime,
                                        theHours,
                                        theMinutes,
                                        theDurationInMinutes,
                                        category
                                        );
                                }
                                else
                                {
                                    ShowEventCreationError("Category not chosen.");
                                }
                            }
                            else
                            {
                                ShowEventCreationError("Duration invalid.");
                            }
                        }
                        else
                        {
                            ShowEventCreationError("Minutes not chosen.");
                        }
                    }
                    else
                    {
                        ShowEventCreationError("Hours not chosen.");
                    }
                }
                else
                {
                    ShowEventCreationError("Date not chosen.");
                }
            }
            else
            {
                ShowEventCreationError("Details not given.");
            }
        }

        //==============================================
        //  Interface methods
        //==============================================
        public void GiveListOfCategories(List<string> categories)
        {
            Categories.Items.Clear();
            foreach (string category in categories)
            {
                ComboBoxItem categoryComboBoxItem = new ComboBoxItem();

                categoryComboBoxItem.Content = category;

                Categories.Items.Add(categoryComboBoxItem);
            }
        }

        public void ClearEventForm()
        {
            throw new NotImplementedException();
        }

        public void DoubleCheckCloseEventForm()
        {
            throw new NotImplementedException();
        }

        public void ShowCreateCategoryFormFromEventForm()
        {
            throw new NotImplementedException();
        }

        public void ShowEventCreated()
        {
            MessageBoxResult userChoice = System.Windows.MessageBox.Show("Event has been created.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            _calendarWindow.SwitchForms(CalendarWindow.Interfaces.CalendarView, null);
        }

        public void ShowEventCreationError(string errMessage)
        {
            // Display Error to do with Event Creation user using Message Box.
            MessageBoxResult userChoice = System.Windows.MessageBox.Show(errMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

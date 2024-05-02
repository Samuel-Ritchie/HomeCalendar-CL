﻿using Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public class Presenter
    {
        // Model instance
        // Can only be initialized once user chooses database file.
        HomeCalendar? _Model = null;

        // Interface references
        private IMainView _mainWindow;
        //private IPromptCreationWindow? _promptCreationWindow;
        //private IHomePage _homePage;

        // State of application fields
        private string _filePath = "";
        private string _fileName = "";
        private string _saveToPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Calendars";
        public bool _changesMade = false;

        public Presenter(IMainView mainWindow)
        {
            // Initialize all existing windows.
            _mainWindow = mainWindow;

        }

        //==============================================
        //  Filtering and Sorting methods
        //==============================================
        public List<CalendarItem> SortEvents(DateTime? startDate, DateTime? endDate, bool FilterFlag, int CategoryID)
        {
            //if (isByMonthCheck == false && isByCategoryCheck == false)
            //{
            return _Model.GetCalendarItems(startDate, endDate, FilterFlag, CategoryID);
            //    home.UpdateEventsGetCalendarItems(calendarItems);

            //} else if (isByMonthCheck == false && isByCategoryCheck == true)
            //{
            //    List<CalendarItemsByCategory> calendarItemsByCategory = _Model.GetCalendarItemsByCategory(startDate, endDate, FilterFlag, CategoryID);
            //    home.UpdateEventsGetCalendarItemsByCategory(calendarItemsByCategory);
            //} else if (isByMonthCheck == true && isByCategoryCheck == false)
            //{
            //    List<CalendarItemsByMonth> calendarItemsByMonth = _Model.GetCalendarItemsByMonth(startDate, endDate, FilterFlag, CategoryID);
            //    home.UpdateEventsGetCalendarItemsByMonth(calendarItemsByMonth);
            //} else
            //{
            //    List<Dictionary<string, object>> calendarItemsByCategoryAndMonth = _Model.GetCalendarDictionaryByCategoryAndMonth(startDate, endDate, FilterFlag, CategoryID);
            //    home.UpdateEventsGetCalendarItemsByCategoryAndMonth(calendarItemsByCategoryAndMonth);
            //}
        }

        //==============================================
        //  Presenter to Model Methods
        //==============================================
        public void InitializeHomeCalendar(string databaseName, string filePath, bool isNewDatabase)
        {
            _filePath = filePath;
            _fileName = databaseName;
            _Model = new HomeCalendar(_filePath, isNewDatabase);

            // Open new window with view function.
            _mainWindow.ShowCalendarInteractivity();

            // _mainWindow will close after this.
        }
        public void SaveChangesAndClose()
        {
            // Close the database file.
            if (_Model is not null)
            {
                // User chose to save their changes that have been made.
                // Use saveLocation and add file name to end.
                _Model.SaveToFile(_saveToPath + "\\" + _fileName);

                // Close the database
                _Model.CloseDB();
                _Model = null;
            }

            // Window closes after this.
        }

        //==============================================
        //  Choose Calendar File Methods
        //==============================================
        public void ProcessDatabaseFile(string databaseName, string filePath, bool isNewDatabase)
        {
            if (isNewDatabase && File.Exists(filePath))
            {
                // User chose create new but file already exists.
                _mainWindow.ShowMainError("Error: database file already exists.");
            }
            else if (isNewDatabase && !File.Exists(filePath))
            {
                // User chose create new and file doesn't already exist.
                InitializeHomeCalendar(databaseName, filePath, isNewDatabase);
            }
            else
            {
                // User wants to open existing database.
                InitializeHomeCalendar(databaseName, filePath, isNewDatabase);
            }
        }

        //==============================================
        //  PromptCreate Methods
        //==============================================
        public void PromptCreateEvent(IPromptCreationWindow View)
        {
            // Call View method to show Event form page.
            View.ShowCreateEventForm();
        }
        public void PromptCreateCategory(IPromptCreationWindow View)
        {
            // Call View method to show Category form page.
            View.ShowCreateCategoryForm();
        }
        public void PromptCreateWindowClosing(IPromptCreationWindow View)
        {
            // Pass back changes made.
            View.AskToSaveOrDiscardPromptCreate(_changesMade);
        }

        //==============================================
        //  CreateEvent Methods
        //==============================================
        public void processEventForm(IEventForm View, string details, DateTime date, int hours, int minutes, int durationMinutes, string category)
        {
            if (_Model is not null)
            {
                date = date.AddHours(hours).AddMinutes(minutes);

                List<Calendar.Category> list = _Model.categories.List();

                int id = -1;

                // Should always find the id.
                foreach (Category currentCataegory in list)
                {
                    if (currentCataegory.Description == category)
                    {
                        id = currentCataegory.Id;
                        break;
                    }
                }

                if (id != -1)
                {
                    _Model.events.Add(date, id, durationMinutes, details);
                    // If Event added successfully
                    // Set _changesMade to true
                    _changesMade = true;
                    View.ShowEventCreated();
                }
                else
                {
                    View.ShowEventCreationError("This Error should be unreachable since all Categories are added to the combobox from the same list. If You are seeing this, welp...");
                }
            }
        }

        public List<Category> GetListOfCategories()
        {
            return _Model.categories.List();
        }

        //==============================================
        //  CreateCategories Methods
        //==============================================
        public void processCategoryForm(ICategoryForm View, string categoryType, string description)
        {
            if (_Model is not null)
            {
                if (categoryType == "Event")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.Event);

                    // If Category added successfully
                    // Set _changesMade to true
                    _changesMade = true;

                    View.ShowCategoryCreated();
                }
                else if (categoryType == "Availibility")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.Availability);

                    // If Category added successfully
                    // Set _changesMade to true
                    _changesMade = true;

                    View.ShowCategoryCreated();
                }
                else if (categoryType == "All Day Event")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.AllDayEvent);

                    // If Category added successfully
                    // Set _changesMade to true
                    _changesMade = true;

                    View.ShowCategoryCreated();
                }
                else if (categoryType == "Holiday")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.Holiday);

                    // If Category added successfully
                    // Set _changesMade to true
                    _changesMade = true;

                    View.ShowCategoryCreated();
                }
                else
                {
                    // This is mor for debugging.
                    View.ShowCategoryCreationError("Some error I guess.");
                }
            }
        }

        //==============================================
        //  Location Picker Methods
        //==============================================

        public void ProcessLocation(IMainView MainView, ILocationPicker View, string location)
        {
            if (Directory.Exists(location))
            {
                _saveToPath = location;

                MainView.ShowCalendarInteractivity();
            }
            else
            {
                View.ShowErrorLocationPicker("Directory does not exist.");
            }
        }
    }
}

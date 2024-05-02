using Calendar;
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
        private string _folderPath = "";
        private string _fileName = "";
        private string _fullPath = "";

        // Create new calendar.
        private string _saveToPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Calendars";

        // Old code, see where this value is implemented and remove.

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
        public void InitializeHomeCalendar(string databaseName, string folderPath, bool isNewDatabase)
        {
            if (isNewDatabase)
            {
                _fileName = databaseName;
                _folderPath = folderPath;
                
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (!folderPath.EndsWith(".db"))
                {
                    _fullPath = _folderPath + $"\\{databaseName}";
                }
                else
                {
                    _fullPath = _folderPath;
                }
            }
            else
            {
                // Database already exists
                // folderPath and fullPath both hold the path and name together.
                _fileName = databaseName;
                _folderPath = folderPath;
                _fullPath = folderPath;
            }

            // Create instance (Open Calendar)
            _Model = new HomeCalendar(_fullPath, isNewDatabase);
            // var a = _Model.categories.List();

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

                // Close the database (Save automatically)
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
            else
            {
                InitializeHomeCalendar(databaseName, filePath, isNewDatabase);
            }
        }
        public void getCurrentLocation()
        {
            // Return data field


            // default is documents.
            _mainWindow.RecieveCurrentSaveLocation(_saveToPath);
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
            View.AskToSaveOrDiscardPromptCreate();
        }

        //==============================================
        //  CreateEvent Methods
        //==============================================
        public void processEventForm(IEventForm View, string details, DateTime date, int hours, int minutes, int durationMinutes, Category category)
        {
            if (_Model is not null)
            {
                date = date.AddHours(hours).AddMinutes(minutes);

                _Model.events.Add(date, category.Id, durationMinutes, details);
                // If Event added successfully
                View.ShowEventCreated();
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
                    View.ShowCategoryCreated();
                }
                else if (categoryType == "Availibility")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.Availability);

                    // If Category added successfully
                    View.ShowCategoryCreated();
                }
                else if (categoryType == "All Day Event")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.AllDayEvent);

                    // If Category added successfully
                    View.ShowCategoryCreated();
                }
                else if (categoryType == "Holiday")
                {
                    _Model.categories.Add(description, Calendar.Category.CategoryType.Holiday);

                    // If Category added successfully
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
            }
            else
            {
                View.ShowErrorLocationPicker("Directory does not exist.");
            }
        }
    }
}

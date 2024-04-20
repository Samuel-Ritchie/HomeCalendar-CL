using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar;

namespace PresenterInterfaceClasses
{
    public class presenter
    {
        // Model instance
        // Can only be initialized once user chooses database file.
        HomeCalendar? _Model = null;

        // Interface references
        private IMainView _mainWindow;

        public presenter(IMainView mainWindow)
        {
            // Initialize all existing windows.
            _mainWindow = mainWindow;
        }

        //==============================================
        //  Presenter to Model Methods
        //==============================================
        public void InitializeHomeCalendar(string databaseName, string filePath, bool isNewDatabase)
        {
            _Model = new HomeCalendar(filePath, isNewDatabase);

            // Open new window with view function.
            _mainWindow.ShowCalendarInteractivity();
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
        public void CloseCalendar(IPromptCreationWindow View)
        {
            // Call View method to ask user if they are sure they want to close calendar.
            View.DoubleCheckCloseApplication();
        }

        //==============================================
        //  CreateEvent Methods
        //==============================================
        public void processEventForm(Event EventToCreate)
        {

        }

        //==============================================
        //  CreateCategories Methods
        //==============================================
        public void processCategoryForm(Category CategoryToCreate)
        {

        }
    }
}

using Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterCode
{
    public class presenter
    {
        // Model instance
        // Can only be initialized once user chooses database file.
        HomeCalendar _Model = null;

        // Interface references
        private ImainWindow _mainWindow;

        public presenter(ImainWindow mainWindow) 
        {
            // Initialize all existing windows.
            _mainWindow = mainWindow;
        }

        //==============================================
        //  Presenter Methods
        //==============================================

        //==============================================
        //  Presenter to Model Methods
        //==============================================

        //==============================================
        //  MainWindow / Choose Calendar File Methods
        //==============================================
        public void InitializeHomeCalendar(string databaseName, string filePath, bool isNewDatabase)
        {
            if (isNewDatabase && !File.Exists(filePath)) 
            {
                // Create new database
                _Model = new HomeCalendar(filePath, isNewDatabase);
            }
            else if (isNewDatabase && File.Exists(filePath))
            {
                // Show already exists error.
                _mainWindow.ShowError("Error: database file already exists.");
            }
            else
            {
                // 
                _Model = new HomeCalendar(filePath, isNewDatabase);
            }

            // Prepare prompt window.
            //_promptCreateWindow.ChangeDisplayInfo(filePath);

            // Open new window with view function.
            _mainWindow.OpenPromptCreateWindow();
        }

        //==============================================
        //  PromptCreate Methods
        //==============================================

        //==============================================
        //  CreateEvent Methods
        //==============================================

        //==============================================
        //  CreateCategories Methods
        //==============================================
    }
}

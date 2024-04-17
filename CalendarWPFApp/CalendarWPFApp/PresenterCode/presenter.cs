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
        HomeCalendar _model = null;

        // Interface references
        private ImainWindow _mainWindow;
        private IpromptCreateWindow _promptCreateWindow;
        private IcreateEventWindow _createEventWindow;
        private IcreateCategoryWindow _createCategoryWindow;

        public presenter(ImainWindow mainWindow, IpromptCreateWindow promptCreateWindow, IcreateEventWindow createEventWindow, IcreateCategoryWindow createCategoryWindow) 
        {
            // Initialize all existing windows.
            _mainWindow = mainWindow;
            _promptCreateWindow = promptCreateWindow;
            _createEventWindow = createEventWindow;
            _createCategoryWindow = createCategoryWindow;
        }

        //==============================================
        //  MainWindow / Choose Calendar File Methods
        //==============================================
        public void CreateNewDatabase()
        {
            // Initialize new HomeCalendar.
        }

        public void OpenExistingDatabase()
        {
            // Initialize new HomeCalendar.
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

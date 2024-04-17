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
        //  Presenter Methods
        //==============================================

        //==============================================
        //  Presenter to Model Methods
        //==============================================

        //==============================================
        //  MainWindow / Choose Calendar File Methods
        //==============================================
        public void InitializeHomeCalendar(string filePath, bool isNewDatabase)
        {
            _Model = new HomeCalendar(filePath, isNewDatabase);
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

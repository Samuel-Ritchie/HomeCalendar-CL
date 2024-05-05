using PresenterInterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterTests
{
    internal class MocCategoryFormView : ICategoryForm
    {
        public Presenter _presenter;

        public MocPromptView _calendarWindow;
        // MocEventForm? _eventWindow;
        // private bool _initializedFromCalendarWindow;

        public MocCategoryFormView(Presenter presenter, MocPromptView calendarWindow /*, CreateEventPage? eventWindow */)
        {
            _presenter = presenter;
            _calendarWindow = calendarWindow;


            // _initializedFromCalendarWindow = true;
            /*
            if (eventWindow is not null)
            {
                _initializedFromCalendarWindow = false;
                _eventWindow = eventWindow;
            }
            */
        }

        // =================================
        //  Interface Fields
        // =================================
        public bool _wasCalled_ShowCategoryCreated = false;
        public bool _wasCalled_ShowCategoryCreationError = false;

        public void ShowCategoryCreated()
        {
            _wasCalled_ShowCategoryCreated = true;
        }

        public void ShowCategoryCreationError(string errMessage)
        {
            _wasCalled_ShowCategoryCreationError = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresenterInterfaceClasses;

namespace PresenterTests
{
    internal class MocEventFormView : IEventForm
    {
        public MocPromptView _calendarWindow;
        public Presenter _presenter;

        public MocEventFormView(Presenter presenter, MocPromptView CalendarWindow)
        {
            _presenter = presenter;
            _calendarWindow = CalendarWindow;
        }


        // =================================
        //  Interface Fields
        // =================================
        public bool _wasCalled_GiveListOfCategories = false;
        public bool _wasCalled_ShowEventCreated = false;
        public bool _wasCalled_ShowEventCreationError = false;

        /*
        public void GiveListOfCategories(List<string> categories)
        {
            _wasCalled_GiveListOfCategories = true;
        }
        */

        public void ShowEventCreated()
        {
            _wasCalled_ShowEventCreated = true;
        }

        public void ShowEventCreationError(string errMessage)
        {
            _wasCalled_ShowEventCreationError = true;
        }
    }
}

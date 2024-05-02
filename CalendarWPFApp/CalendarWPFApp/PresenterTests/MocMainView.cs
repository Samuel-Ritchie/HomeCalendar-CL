using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresenterInterfaceClasses;

namespace PresenterTests
{
    internal class MocMainView : IMainView
    {
        public Presenter _presenter;

        public MocMainView()
        {
            _presenter = new Presenter(this);

            SetUpPageForNewDatabase();
        }

        public void SetUpPageForNewDatabase()
        {
            // Get current save location from presenter. VVV
            _presenter.getCurrentLocation();
        }

        // =================================
        //  Interface Fields
        // =================================
        public bool _wasCalled_RecieveCurrentSaveLocation = false;
        public bool _wasCalled_ShowCalendarInteractivity = false;
        public bool _wasCalled_ShowMainError = false;

        public void RecieveCurrentSaveLocation(string location)
        {
            _wasCalled_RecieveCurrentSaveLocation = true;
        }

        public void ShowCalendarInteractivity()
        {
            _wasCalled_ShowCalendarInteractivity = true;
        }

        public void ShowMainError(string errMsg)
        {
            _wasCalled_ShowMainError = true;
        }
    }
}

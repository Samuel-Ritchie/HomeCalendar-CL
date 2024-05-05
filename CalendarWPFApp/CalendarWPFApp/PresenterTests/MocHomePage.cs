using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar;
using PresenterInterfaceClasses;

namespace PresenterTests
{
    internal class MocHomePage : IHomePage
    {

        public Presenter _presenter;

        public MocHomePage(Presenter presenter)
        {
            _presenter = presenter;
        }

        // =================================
        //  Interface Fields
        // =================================

        public bool _wasCalled_SetCalendarItems = false;
        public bool _wasCalled_SetCalendarItemsByMonth = false;
        public bool _wasCalled_SetCalendarItemsByCategory = false;
        public bool _wasCalled_SetCalendarItemsByMonthAndCategory = false;

        public void SetCalendarItems(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            _wasCalled_SetCalendarItems = true;
        }
        public void SetCalendarItemsByMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            _wasCalled_SetCalendarItemsByMonth = true;
        }
        public void SetCalendarItemsByCategory(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            _wasCalled_SetCalendarItemsByCategory = true;
        }
        public void SetCalendarItemsByCategoryAndMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0)
        {
            _wasCalled_SetCalendarItemsByMonthAndCategory = true;
        }
    }
}

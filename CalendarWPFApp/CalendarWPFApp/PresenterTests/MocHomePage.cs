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










        // =================================
        //  Interface Fields
        // =================================

        public bool _wasCalled_UpdateEventsGetCalendarItems = false;
        public bool _wasCalled_UpdateEventsGetCalendarItemsByCategory = false;
        public bool _wasCalled_UpdateEventsGetCalendarItemsByCategoryAndMonth = false;
        public bool _wasCalled_UpdateEventsGetCalendarItemsByMonth = false;

        public void UpdateEventsGetCalendarItems(List<CalendarItem> calendarItems)
        {
            _wasCalled_UpdateEventsGetCalendarItems = true;
        }

        public void UpdateEventsGetCalendarItemsByCategory(List<CalendarItemsByCategory> calendarItemsByCategory)
        {
            _wasCalled_UpdateEventsGetCalendarItemsByCategory = true;
        }

        public void UpdateEventsGetCalendarItemsByCategoryAndMonth(List<Dictionary<string, object>> calendarItemsByCatAndMonth)
        {
            _wasCalled_UpdateEventsGetCalendarItemsByCategoryAndMonth = true;
        }

        public void UpdateEventsGetCalendarItemsByMonth(List<CalendarItemsByMonth> calendarItemsByMonth)
        {
            _wasCalled_UpdateEventsGetCalendarItemsByMonth = true;
        }
    }
}

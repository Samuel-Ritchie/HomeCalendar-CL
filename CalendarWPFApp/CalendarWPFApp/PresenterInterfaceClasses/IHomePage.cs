using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface IHomePage
    {
        //methods for updating the datagrid resource
        public void UpdateEventsGetCalendarItems(List<Calendar.CalendarItem> calendarItems);
        public void UpdateEventsGetCalendarItemsByCategory(List<Calendar.CalendarItemsByCategory> calendarItemsByCategory);
        public void UpdateEventsGetCalendarItemsByMonth(List<Calendar.CalendarItemsByMonth> calendarItemsByMonth);
        public void UpdateEventsGetCalendarItemsByCategoryAndMonth(List<Dictionary<string, object>> calendarItemsByCatAndMonth);


    }
}

using Calendar;
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
        public void SetCalendarItems(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0);
        public void SetCalendarItemsByMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0);
        public void SetCalendarItemsByCategory(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0);
        public void SetCalendarItemsByCategoryAndMonth(DateTime? start = null, DateTime? end = null, bool filter = false, int searchCategory = 0);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface IMainView
    {
            public void ShowMainError(string errMsg);

            // Switch to Prompt Window.
            public void ShowCalendarInteractivity();
    }
}

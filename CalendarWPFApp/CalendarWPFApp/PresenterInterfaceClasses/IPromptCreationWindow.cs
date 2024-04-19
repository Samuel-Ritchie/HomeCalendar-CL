using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface IPromptCreationWindow
    {
        // Switch to EventFormView.
        public void ShowCreateEventForm();

        // Switch to CategoryFormView.
        public void ShowCreateCategoryForm();

        // Pop up window asking user if they are sure they want to close their calendar and the app itself.
        public void DoubleCheckCloseApplication();
    }
}

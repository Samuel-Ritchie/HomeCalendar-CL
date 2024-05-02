using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface IEventForm
    {
        // Pop up a window that tells the user that their event was created.
        public void ShowEventCreated();

        // Pop up an error message window that tells the user that they submitted the form wrong.
        public void ShowEventCreationError(string errMessage);

        // Used to get list of categories to select from
        public void GiveListOfCategories(List<string> categories);
    }
}

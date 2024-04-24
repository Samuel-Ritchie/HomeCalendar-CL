using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface IEventForm
    {
        // Once user closes the form, pop up window asking if they really want to exit, risking losing unsaved changes.
        public void DoubleCheckCloseEventForm();

        // Clear any input in the Form once Event is successfully created.
        public void ClearEventForm();

        // Pop up a window that tells the user that their event was created.
        public void ShowEventCreated();

        // Pop up an error message window that tells the user that they submitted the form wrong.
        public void ShowEventCreationError(string errMessage);

        // Switch to CategoryCreationView while in EventCreationView
        public void ShowCreateCategoryFormFromEventForm();

        // Used to get list of categories to select from
        public void GiveListOfCategories(List<string> categories);
    }
}

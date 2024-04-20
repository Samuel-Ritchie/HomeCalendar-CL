using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface ICategoryForm
    {
        // Once user closes the form, pop up window asking if they really want to exit, risking losing unsaved changes.
        public void DoubleCheckCloseCategoryForm();

        // Clear any input in the Form once Category is successfully created.
        public void ClearCategoryForm();

        // Pop up a window that tells the user that their category was created.
        public void ShowCategoryCreated();

        // Pop up an error message window that tells the user that they submitted the form wrong.
        public void ShowCategoryCreationError();

        // Switch to CategoryCreationView while in EventCreationView
        public void ShowCreateCategoryForm();

        // Close Category form after category is successfully created in the case that the user created a Category while creating an event.
        public void ReturnToEventForm();
    }
}

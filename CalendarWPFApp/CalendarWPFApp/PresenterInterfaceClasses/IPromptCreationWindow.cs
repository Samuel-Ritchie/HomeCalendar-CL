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

        // Presenter calls method to ask user if they really want to quit. (the Closing event handler is called when red X button is pressed)
        public void AskToSaveOrDiscardPromptCreate(bool changesMade);

        
    }
}

using PresenterInterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterTests
{
    internal class MocPromptView : IPromptCreationWindow
    {

        public Presenter _presenter;

        // To keep track of which screen the user is being faced with.
        public enum Interfaces
        {
            ChooseToCreate = 1,
            CreateEvent = 2,
            CreateCategory = 3,
            CalendarView = 4,
        }

        public MocPromptView(Presenter presenter)
        {
            _presenter = presenter;
        }

        // =================================
        //  Interface Fields
        // =================================
        public bool _wasCalled_AskToSaveOrDiscardPromptCreate = false;
        public bool _wasCalled_ShowCreateCategoryForm = false;
        public bool _wasCalled_ShowCreateEventForm = false;

        public void AskToSaveOrDiscardPromptCreate()
        {
            _wasCalled_AskToSaveOrDiscardPromptCreate = true;
        }

        public void ShowCreateCategoryForm()
        {
            _wasCalled_ShowCreateCategoryForm = true;
        }

        public void ShowCreateEventForm()
        {
            _wasCalled_ShowCreateEventForm = true;
        }
    }
}

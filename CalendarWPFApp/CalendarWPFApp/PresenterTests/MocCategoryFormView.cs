using PresenterInterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterTests
{
    internal class MocCategoryFormView : ICategoryForm
    {



        // =================================
        //  Interface Fields
        // =================================
        public bool _wasCalled_ShowCategoryCreated = false;
        public bool _wasCalled_ShowCategoryCreationError = false;

        public void ShowCategoryCreated()
        {
            _wasCalled_ShowCategoryCreated = true;
        }

        public void ShowCategoryCreationError(string errMessage)
        {
            _wasCalled_ShowCategoryCreationError = true;
        }
    }
}

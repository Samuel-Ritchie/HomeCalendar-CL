using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterInterfaceClasses
{
    public interface ICategoryForm
    {
        // Pop up a window that tells the user that their category was created.
        public void ShowCategoryCreated();

        // Pop up an error message window that tells the user that they submitted the form wrong.
        public void ShowCategoryCreationError(string errMessage);
    }
}

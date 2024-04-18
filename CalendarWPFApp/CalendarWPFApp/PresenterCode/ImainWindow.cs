using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterCode
{
    public interface ImainWindow
    {
        public void OpenPromptCreateWindow();

        public void ShowError(string errorMsg);
    }
}
using NuGet.Frameworks;

namespace PresenterTests
{
    public class MainTests
    {
        // ===========================
        //  Main View
        // ===========================
        [Fact]
        public void RecieveCurrentSaveLocationIsCalled()
        {
            MocMainView a = new MocMainView();

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);
        }

        [Fact]
        public void ShowCalendarInteractivityWasCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);
        }

        [Fact]
        public void ShowMainErrorWasCalled_existingDatabase_FileNotFound()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInpppppput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_ShowMainError);
        }

        [Fact]
        public void ShowMainErrorWasCalled_newDatabase_FileAlreadyExists()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string folderPath = "..\\..\\..\\Calendars";
            bool isNewDatabase = true;

            a._presenter.ProcessDatabaseFile(fileName, folderPath, isNewDatabase);

            Assert.True(a._wasCalled_ShowMainError);
        }

        // ===========================
        //  Prompt View
        // ===========================
        [Fact]
        public void AskToSaveOrDiscardPromptCreateIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);


            MocPromptView b = new MocPromptView(a._presenter);

            Assert.True(b._wasCalled_AskToSaveOrDiscardPromptCreate);

            // Not complete
        }
    }
}
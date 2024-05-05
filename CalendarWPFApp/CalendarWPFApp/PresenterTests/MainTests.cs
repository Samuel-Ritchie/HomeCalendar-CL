using Calendar;
using NuGet.Frameworks;
using PresenterInterfaceClasses;
using static Calendar.Category;

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

            b._presenter.PromptCreateWindowClosing(b);

            Assert.True(b._wasCalled_AskToSaveOrDiscardPromptCreate);
        }

        [Fact]
        public void ShowCreateEventFormIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            b._presenter.PromptCreateEvent(b);

            Assert.True(b._wasCalled_ShowCreateEventForm);
        }

        [Fact]
        public void ShowCreateCategoryFormIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            b._presenter.PromptCreateCategory(b);

            Assert.True(b._wasCalled_ShowCreateCategoryForm);
        }

        // ===========================
        //  HomePage View
        // ===========================
        [Fact]
        public void UpdateEventsGetCalendarItemsIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            MocHomePage c = new MocHomePage(b._presenter);

            c._presenter.SortEvents(null, null, false, 0);

            // Assert.True(c._wasCalled_ShowEventCreated);

            // Further implementation needed in HomePage.cs
        }


        // ===========================
        //  CreateEventForm View
        // ===========================

        /*
        [Fact]
        public void GiveListOfCategoriesIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            MocEventFormView c = new MocEventFormView(a._presenter, b);

            c._presenter.PromptCreateCategory();

            Assert.True(b._wasCalled_GiveListOfCategories);
        }
        */

        [Fact]
        public void ShowEventCreatedIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            MocEventFormView c = new MocEventFormView(b._presenter, b);

            c._presenter.processEventForm(
            c, "Test Event", DateTime.Now, 1, 1, 60, c._presenter._Model.categories.List()[0]
            );

            Assert.True(c._wasCalled_ShowEventCreated);
        }
        // ===========================
        //  CreateCategoryForm View
        // ===========================
        [Fact]
        public void ShowCategoryCreatedIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            MocCategoryFormView c = new MocCategoryFormView(b._presenter, b);

            c._presenter.processCategoryForm(c, "Event", "Test Category");

            Assert.True(c._wasCalled_ShowCategoryCreated);
        }
        [Fact]
        public void ShowCategoryCreationErrorIsCalled()
        {
            MocMainView a = new MocMainView();

            string fileName = "testDBInput.db";
            string fullPath = "..\\..\\..\\Calendars\\testDBInput.db";
            bool isNewDatabase = false;

            a._presenter.ProcessDatabaseFile(fileName, fullPath, isNewDatabase);

            Assert.True(a._wasCalled_RecieveCurrentSaveLocation);

            MocPromptView b = new MocPromptView(a._presenter);

            MocCategoryFormView c = new MocCategoryFormView(b._presenter, b);

            c._presenter.processCategoryForm(c, "Invalid Category type string.", "Test Category");

            Assert.True(c._wasCalled_ShowCategoryCreationError);
        }
    }
}
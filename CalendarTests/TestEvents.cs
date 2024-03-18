using System;
using Xunit;
using System.IO;
using System.Collections.Generic;
using Calendar;
using System.Data.SQLite;

namespace CalendarCodeTests
{
    public class TestEvents
    {
        int numberOfEventsInFile = TestConstants.numberOfEventsInFile;
        String testInputFile = TestConstants.testEventsInputFile;
        int maxIDInEventFile = TestConstants.maxIDInEventFile;
        Event firstEventInFile = new Event(1, new DateTime(2021, 1, 10), 3, 40, "App Dev Homework");


        // ========================================================================

        [Fact]
        public void EventsObject_New()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String newDB = $"{folder}\\newDB.db";
            Database.newDatabase(newDB);
            SQLiteConnection conn = Database.dbConnection;

            // Act
            Events Events = new Events(conn, true);

            // Assert 
            Assert.IsType<Events>(Events);
        }


        // ========================================================================

        [Fact]
        public void EventsMethod_ReadFromDatabase_ValidateCorrectDataWasRead()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String existingDB = $"{folder}\\{TestConstants.testDBInputFile}";
            Database.existingDatabase(existingDB);
            SQLiteConnection conn = Database.dbConnection;

            // Act
            Events events = new Events(conn, false);
            List<Event> list = events.List();
            Event firstEvent = list[0];

            // Assert
            Assert.Equal(numberOfEventsInFile, list.Count);
            Assert.Equal(firstEventInFile.Id, firstEvent.Id);
            Assert.Equal(firstEventInFile.DurationInMinutes, firstEvent.DurationInMinutes);
            Assert.Equal(firstEventInFile.Details, firstEvent.Details);
            Assert.Equal(firstEventInFile.Category, firstEvent.Category);

        }

        // ========================================================================

        [Fact]
        public void EventsMethod_List_ReturnsListOfEvents()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String newDB = $"{folder}\\{TestConstants.testDBInputFile}";
            Database.existingDatabase(newDB);
            SQLiteConnection conn = Database.dbConnection;
            Events Events = new Events(conn, false);

            // Act
            List<Event> list = Events.List();

            // Assert
            Assert.Equal(numberOfEventsInFile, list.Count);

        }

        // ========================================================================

        [Fact]
        public void EventsMethod_Add()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String goodDB = $"{folder}\\{TestConstants.testDBInputFile}";
            String messyDB = $"{folder}\\messy.db";
            System.IO.File.Copy(goodDB, messyDB, true);
            Database.existingDatabase(messyDB);
            SQLiteConnection conn = Database.dbConnection;
            Events Events = new Events(conn, false);
            string descr = "New Category";
            Category.CategoryType type = Category.CategoryType.Event;

            int category = 1;
            double DurationInMinutes = 98.1;

            // Act
            Events.Add(DateTime.Now, category, DurationInMinutes, "new Event");
            List<Event> EventsList = Events.List();
            int sizeOfList = Events.List().Count;

            // Assert
            Assert.Equal(numberOfEventsInFile + 1, sizeOfList);
            Assert.Equal(maxIDInEventFile + 1, EventsList[sizeOfList - 1].Id);
            Assert.Equal(DurationInMinutes, EventsList[sizeOfList - 1].DurationInMinutes);
        }

        // ========================================================================

        [Fact]
        public void EventsMethod_Delete()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String goodDB = $"{folder}\\{TestConstants.testDBInputFile}";
            String messyDB = $"{folder}\\messy.db";
            System.IO.File.Copy(goodDB, messyDB, true);
            Database.existingDatabase(messyDB);
            SQLiteConnection conn = Database.dbConnection;
            Events Events = new Events(conn, false);
            int IdToDelete = 3;

            // Act
            Events.Delete(IdToDelete);
            List<Event> EventsList = Events.List();
            int sizeOfList = EventsList.Count;

            // Assert
            Assert.Equal(numberOfEventsInFile - 1, sizeOfList);
            Assert.False(EventsList.Exists(e => e.Id == IdToDelete), "correct Event item deleted");

        }

        // ========================================================================

        [Fact]
        public void EventsMethod_Delete_InvalidIDDoesntCrash()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String goodDB = $"{folder}\\{TestConstants.testDBInputFile}";
            String messyDB = $"{folder}\\messyDB";
            System.IO.File.Copy(goodDB, messyDB, true);
            Database.existingDatabase(messyDB);
            SQLiteConnection conn = Database.dbConnection;
            Events Events = new Events(conn, false);
            int IdToDelete = 1006;
            int sizeOfList = Events.List().Count;

            // Act
            try
            {
                Events.Delete(IdToDelete);
                Assert.Equal(sizeOfList, Events.List().Count);
            }

            // Assert
            catch
            {
                Assert.True(false, "Invalid ID causes Delete to break");
            }
        }

        // ========================================================================

        [Fact]
        public void EventsMethod_UpdateEvents()
        {
            // Arrange
            String folder = TestConstants.GetSolutionDir();
            String goodDB = $"{folder}\\{TestConstants.testDBInputFile}";
            String messyDB = $"{folder}\\messyDB";
            System.IO.File.Copy(goodDB, messyDB, true);
            Database.existingDatabase(messyDB);
            SQLiteConnection conn = Database.dbConnection;
            Events events = new Events(conn, true);
            //         public void UpdateProperties(int Id, DateTime date, int category, Double duration, String details)
            int id = 5;
            DateTime date = DateTime.Now;
            int categoryId = 0;
            double duration = 60;
            string details = "Staff party";


            // Act
            events.UpdateProperties(id, date, categoryId, duration, details);
            string eventsList = "";
            string updated = 
                id + ", " +
                date + ", " +
                categoryId + ", " +
                duration + ", " +
                details;

            foreach(Event e in events.List()) 
            {
                string eventProperties = 
                    e.Id + ", " +
                    e.StartDateTime + ", " +
                    e.Category + ", " +
                    e.DurationInMinutes + ", " +
                    e.Details;

                eventsList += eventProperties + "\n";
            }

            // Assert 
            Assert.Contains(updated, eventsList);
        }
    }
}


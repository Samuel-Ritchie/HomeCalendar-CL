using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Data.Common;
using System.Data.SQLite;

// ============================================================================
// (c) Sandy Bultena 2018
// * Released under the GNU General Public License
// ============================================================================

namespace Calendar
{
    // ====================================================================
    // CLASS: Events
    //        - A collection of Event items,
    //        - Read / write to file
    //        - etc
    // ====================================================================
    public class Events
    {
        private SQLiteConnection? _connection;

        // ====================================================================
        // Properties
        // ====================================================================
        public SQLiteConnection Connection { get { return _connection; } }

        // ====================================================================
        // Constructor
        // ====================================================================
        /// <summary>
        /// AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH.
        /// </summary>
        public Events(SQLiteConnection connection = null, bool isnewDb = false)
        {
            _connection = connection;
        }

        // ====================================================================
        // Add Event
        // ====================================================================
        public void Add(DateTime date, int category, Double duration, String details)
        {
            //Id and CategoryId?
            
           SQLiteCommand cmd = new SQLiteCommand("INSERT INTO events (CategoryId, StartDateTime, Details, DurationInMinutes) " +
               "VALUES (@category, @startDate, @details, @duration);", Connection);

            cmd.Parameters.AddWithValue("@startDate", date.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@duration", duration);
            cmd.Parameters.AddWithValue("@details", details);

            cmd.ExecuteNonQuery();
        }

        // ====================================================================
        // Update Event
        // ====================================================================
        public void UpdateProperties(int id, DateTime date, int category, Double duration, String details)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE events SET " +
                "CategoryId = @categoryId, " +
                "StartDateTime = @startDate, " +
                "Details = @details, " +
                "DurationInMinutes = @duration " +
                "WHERE Id = @id;", Connection);


            cmd.Parameters.AddWithValue("@categoryId", category);
            cmd.Parameters.AddWithValue("@startDate", date.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@duration", duration);
            cmd.Parameters.AddWithValue("@details", details);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        // ====================================================================
        // Delete Event
        // ====================================================================
        public void Delete(int id)
        {
            SQLiteCommand cmd = new SQLiteCommand($"DELETE FROM events WHERE Id = @id;", Connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        // ====================================================================
        // Return list of Events
        // Note:  make new copy of list, so user cannot modify what is part of
        //        this instance
        // ====================================================================
        public List<Event> List()
        {
            List<Event> eventList = new List<Event>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, StartDateTime, CategoryId, DurationInMinutes, Details FROM events;", Connection);
            SQLiteDataReader results = cmd.ExecuteReader();

            while (results.Read())
            {
                int id = results.GetInt32(0);
                DateTime date = DateTime.Parse(results.GetString(1)); // results.GetDateTime(1
                int category = results.GetInt32(2);
                double duration = results.GetDouble(3);
                string details = results.GetString(4);

                eventList.Add(new Event(id, date, category, duration, details));
            }

            return eventList;
        }

    }
}


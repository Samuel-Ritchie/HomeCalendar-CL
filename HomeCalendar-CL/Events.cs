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

            if (connection != null && isnewDb)
            {
            }

        }

        // ====================================================================
        // Add Event
        // ====================================================================
        private void Add(Event exp)
        {
            _Events.Add(exp);
        }

        public void Add(DateTime date, int category, Double duration, String details)
        {
            int new_id = 1;

            // if we already have Events, set ID to max
            if (_Events.Count > 0)
            {
                new_id = (from e in _Events select e.Id).Max();
                new_id++;
            }

            _Events.Add(new Event(new_id, date, category, duration, details));

        }

        // ====================================================================
        // Delete Event
        // ====================================================================
        public void Delete(int Id)
        {
            foreach (Event e in _Events) 
            { 
                if (e.Id == Id)
                {
                    _Events.Remove(e);
                    break;
                }
            }
        }

        // ====================================================================
        // Return list of Events
        // Note:  make new copy of list, so user cannot modify what is part of
        //        this instance
        // ====================================================================
        public List<Event> List()
        {
            List<Event> newList = new List<Event>();
            foreach (Event Event in _Events)
            {
                newList.Add(new Event(Event));
            }
            return newList;
        }

    }
}


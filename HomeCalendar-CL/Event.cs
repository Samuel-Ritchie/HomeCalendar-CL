using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ============================================================================
// (c) Sandy Bultena 2018
// * Released under the GNU General Public License
// ============================================================================

namespace Calendar
{
    // ====================================================================
    // CLASS: Event
    //        - An individual event for calendar program
    // ====================================================================
    /// <summary>
    /// Holds the data for an individual event object.
    /// Data includes Event Id, Start Date Time, Duration in minutes, details about the event, the Category it fits into.
    /// </summary>
    public class Event
    {
        // ====================================================================
        // Properties
        // ====================================================================
        /// <summary>
        /// Gets Id of Event Object.
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Gets Start date time of Event Object.
        /// </summary>
        public DateTime StartDateTime { get;  }
        /// <summary>
        /// Gets Duration of Event Object in minutes.
        /// </summary>
        public Double DurationInMinutes { get; set; }
        /// <summary>
        /// Gets Details of the Event Object.
        /// </summary>
        public String Details { get; set; }
        /// <summary>
        /// Gets the Category of an Event Object.
        /// </summary>
        public int Category { get; set; }

        // ====================================================================
        // Constructor
        //    NB: there is no verification the event category exists in the
        //        categories object
        // ====================================================================
        /// <summary>
        /// Creates a new Event Object.
        /// Sets the ID, the StartDate, the CategoryId, the DurationInMinutes, and the Details.
        /// </summary>
        /// <param name="id">The Id of the Event Object.</param>
        /// <param name="date">The Start Date Time of the Event Object.</param>
        /// <param name="category">The Category Id of the Event Object.</param>
        /// <param name="duration">The Duration of the Event Object in minutes.</param>
        /// <param name="details">The Details about the event of the Event Object.</param>
        public Event(int id, DateTime date, int category, Double duration, String details)
        {
            this.Id = id;
            this.StartDateTime = date;
            this.Category = category;
            this.DurationInMinutes = duration;
            this.Details = details;
        }

        // ====================================================================
        // Copy constructor - does a deep copy
        // ====================================================================
        /// <summary>
        /// /// Creates a new Event Object by copying an existing one.
        /// Sets the ID, the StartDate, the CategoryId, the DurationInMinutes, and the Details according to the passed Event Object.
        /// </summary>
        /// <param name="obj">The Event Object to copy.</param>
        public Event (Event obj)
        {
            this.Id = obj.Id;
            this.StartDateTime = obj.StartDateTime;
            this.Category = obj.Category;
            this.DurationInMinutes = obj.DurationInMinutes;
            this.Details = obj.Details;
           
        }
    }
}

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
    // CLASS: CalendarItem
    //        A single calendar item, includes a Category and an Event
    // ====================================================================
    /// <summary>
    /// Holds the data for an individual CalendarItem Object.
    /// Data includes CategoryID, EventID, Start DateTime, Category, Short Description, Duration in minutes, and Busy Time.
    /// </summary>
    public class CalendarItem
    {
        /// <summary>
        /// Gets/Sets CategoryID of CalendarItem Object.
        /// </summary>
        /// <value>The Category ID value to set.</value>
        public int CategoryID { get; set; }
        /// <summary>
        /// Gets/Sets EventID of CalendarItem Object.
        /// </summary>
        /// <value>The Event ID value to set.</value>
        public int EventID { get; set; }
        /// <summary>
        /// Gets/Sets StartDateTime of CalendarItem Object.
        /// </summary>
        /// <value>The StartDateTime Struct to set.</value>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// Gets/Sets Category of CalendarItem Object.
        /// </summary>
        /// <value>The Category string to set.</value>
        public String? Category { get; set; }
        /// <summary>
        /// Gets/Sets Short Description of CalendarItem Object.
        /// </summary>
        /// <value>The Short Description string to set.</value>
        public String? ShortDescription { get; set; }
        /// <summary>
        /// Gets/Sets Duration In Minutes of CalendarItem Object.
        /// </summary>
        /// <value>The Duration In Minutes Double to set.</value>
        public Double DurationInMinutes { get; set; }
        /// <summary>
        /// Gets/Sets Busy Time of CalendarItem Object.
        /// </summary>
        /// <value>The Busy Time Double to set.</value>
        public Double BusyTime { get; set; }

    }
    /// <summary>
    /// Holds the data for a list of CalendarItems grouped by the month they take place in.
    /// Data includes Month, the Item list, and the total busy time.
    /// </summary>
    public class CalendarItemsByMonth
    {
        /// <summary>
        /// Gets/Sets Month of a CalendarItemsByMonth Object.
        /// </summary>
        /// <value>The Month string to set.</value>
        public String? Month { get; set; }
        /// <summary>
        /// Gets/Sets Items List from a CalendarItemsByMonth Object.
        /// </summary>
        /// <value>The List of CalendarItems to set.</value>
        public List<CalendarItem>? Items { get; set; }
        /// <summary>
        /// Gets/Sets Total Busy Time of a CalendarItemsByMonth Object.
        /// </summary>
        /// <value>The Total Busy Time Double to set.</value>
        public Double TotalBusyTime { get; set; }
    }
    /// <summary>
    /// Holds the data for a list of CalendarItems grouped by the Category they fit into.
    /// Data includes Category Name, the Item list, and the total busy time.
    /// </summary>
    public class CalendarItemsByCategory
    {
        /// <summary>
        /// Gets/Sets Category of CalendarItemsByCategory Object.
        /// </summary>
        /// <value>The Category string to set.</value>
        public String? Category { get; set; }
        /// <summary>
        /// Gets/Sets Items List from a CalendarItemsByCategory Object.
        /// </summary>
        /// <value>The List of CalendarItems to set.</value>
        public List<CalendarItem>? Items { get; set; }
        /// <summary>
        /// Gets/Sets Total Busy Time of CalendarItemsByCategory Object.
        /// </summary>
        /// <value>The Total Busy Time Double to set.</value>
        public Double TotalBusyTime { get; set; }

    }
}

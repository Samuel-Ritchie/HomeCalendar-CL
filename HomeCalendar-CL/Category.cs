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
    // CLASS: Category
    //        - An individual category for Calendar program
    //        - Valid category types: Event,AllDayEvent,Holiday
    // ====================================================================
    /// <summary>
    /// Holds the data for an individual Category Object.
    /// Data includes Id, Description, and Category Type.
    /// </summary>
    public class Category
    {
        // ====================================================================
        // Properties
        // ====================================================================
        /// <summary>
        /// Gets Id of Category Object.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets Description of Category Object.
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Gets the Type of a Category Object.
        /// </summary>
        public CategoryType Type { get; set; }
        /// <summary>
        /// Defines the three Types of Categories there can be.
        /// </summary>
        /// 
        public enum CategoryType
        {
            /// <summary>
            /// An event that lasts for less than a day.
            /// </summary>
            Event,
            /// <summary>
            /// An event that lasts all day.
            /// </summary>
            AllDayEvent,
            /// <summary>
            /// A special holiday, can last several days (maybe).
            /// </summary>
            Holiday,
        };

        // ====================================================================
        // Constructor
        // ====================================================================
        /// <summary>
        /// Creates a new Category Object.
        /// Sets the Id, the Description, and the Category Type.
        /// </summary>
        /// <param name="id">The Id of the Category Object.</param>
        /// <param name="description">The Description of the Category Object.</param>
        /// <param name="type">The Type of the Category Object.</param>
        public Category(int id, String description, CategoryType type = CategoryType.Event)
        {
            this.Id = id;
            this.Description = description;
            this.Type = type;
        }

        // ====================================================================
        // Copy Constructor
        // ====================================================================
        /// <summary>
        /// Creates a new Category Object by copying an exitsting one.
        /// Sets the Id, the Description, and the Category Type according to the passed Category Object.
        /// </summary>
        /// <param name="category">The Category Object to Copy.</param>
        public Category(Category category)
        {
            this.Id = category.Id;;
            this.Description = category.Description;
            this.Type = category.Type;
        }
        // ====================================================================
        // String version of object
        // ====================================================================
        /// <summary>
        /// Returns a string describing the Category Object.
        /// </summary>
        /// <returns>(String) Description of the Category Object.</returns>
        public override string ToString()
        {
            return Description;
        }

    }
}


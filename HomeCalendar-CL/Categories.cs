using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Data.SQLite;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Security.Cryptography;
using System.Data;

// ============================================================================
// (c) Sandy Bultena 2018
// * Released under the GNU General Public License
// ============================================================================

namespace Calendar
{

    /// <summary>
    /// Holds a list of Category Objects, and File location info corresponding to a Categories File.
    /// </summary>
    public class Categories
    {
        private SQLiteConnection? _connection;

        // ====================================================================
        // Properties
        // ====================================================================
        public SQLiteConnection Connection{ get { return _connection; } }

        // ====================================================================
        // Constructor
        // ====================================================================
        /// <summary>
        /// Sets the Categories List in the Categories Class with default Categories.
        /// </summary>
        public Categories(SQLiteConnection connection = null, bool isnewDb = false)
        {
            _connection = connection;

            if (connection != null && isnewDb)
            {
                SetCategoryTypes();
                SetCategoriesToDefaults();
            }

        }

        // ====================================================================
        // Methods
        // ====================================================================
        /// <summary>
        /// Gets the Category according to it's Id.
        /// Uses the Id passed as a parameter to select and return a specific Category.
        /// </summary>
        /// <param name="id">The Id number of the Category Object.</param>
        /// <returns>A Category Object with the Id passed.</returns>
        /// <exception cref="Exception">If Id does not correspond to an existing Category.</exception>
        public Category GetCategoryFromId(int id)
        {
            SQLiteCommand cmd = new SQLiteCommand($"SELECT Description, TypeId FROM categories WHERE Id = {id};", Connection);
            SQLiteDataReader result = cmd.ExecuteReader();

            Category? c = null;

            if (result.Read())
                c = new Category(id, result.GetString(0), (Category.CategoryType)result.GetInt32(1));

            return c;
        }

        /// <summary>
        /// Sets the Categories List of Category Objects to an assortment of Default Categories.
        /// </summary>
        public void SetCategoriesToDefaults()
        {
            // ---------------------------------------------------------------
            // reset any current categories,
            // ---------------------------------------------------------------
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM categories;", Connection);
            cmd.ExecuteNonQuery();

            // ---------------------------------------------------------------
            // Add default categories
            // ---------------------------------------------------------------

            Dictionary<string, Category.CategoryType> categoriesToAdd= new Dictionary<string, Category.CategoryType>()
            {
                { "School", Category.CategoryType.Event },
                { "Personal", Category.CategoryType.Event },
                { "VideoGames", Category.CategoryType.Event },
                { "Medical", Category.CategoryType.Event },
                { "Sleep", Category.CategoryType.Event },
                { "Vacation", Category.CategoryType.AllDayEvent },
                { "Travel days", Category.CategoryType.AllDayEvent },
                { "Canadian Holidays", Category.CategoryType.Holiday },
                { "US Holidays", Category.CategoryType.Holiday }
            };

            foreach (KeyValuePair<string, Category.CategoryType> category in categoriesToAdd) 
            {
                Add(category.Key, category.Value);
            }
        }

        private void SetCategoryTypes()
        {
            List<Category.CategoryType> typesToAdd = new List<Category.CategoryType>() 
            {
                Category.CategoryType.Holiday,
                Category.CategoryType.Availability,
                Category.CategoryType.Event,
                Category.CategoryType.AllDayEvent
            };

            SQLiteCommand cmd = new SQLiteCommand(Connection);
            cmd.CommandText = "";

            for (int type = 0; type < typesToAdd.Count; type++)
            {
                cmd.CommandText += "INSERT INTO categoryTypes (Id ,Description) " +
                    $"VALUES({(int)typesToAdd[type]}, '{typesToAdd[type]}'); ";
            }

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds a new Category Object to the Categories list in the Categories Class.
        /// Generates an Id number for a new Category Object.
        /// Creates a Category Object, assigning it the values passed as parameters.
        /// Adds the new Category Object to the list in the Categories Class.
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="type"></param>
        public void Add(String desc, Category.CategoryType type)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO categories (Description ,TypeId) " +
                "VALUES(@desc, @typeId)", Connection);

            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@typeId", (int)type);

            cmd.ExecuteNonQuery();
        }

        public void UpdateProperties(int id, string desc, Category.CategoryType categoryType)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE categories SET " +
                "Description = @desc, " +
                "TypeId = @typeId " +
                "WHERE Id = @id;", Connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@typeId", (int)categoryType);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Removes an existing Category Object from the Categories List in the Categories Class.
        /// Takes in an ID number that determines which event to remove.
        /// </summary>
        /// <param name="Id">(Int) The id of the Category Object to Remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the Id passed is not in the existing range of the list.</exception>
        public void Delete(int id)
        {
            SQLiteCommand cmd = new SQLiteCommand($"DELETE FROM categories WHERE Id = {id}", Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets a list of all Category Objects in the Categories Class list.
        /// </summary>
        /// <returns>A list of all Category Objects in the Categories Class list</returns>
        public List<Category> List()
        {
            List<Category> categories = new List<Category>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Description, TypeId FROM categories;", Connection);
            SQLiteDataReader results = cmd.ExecuteReader();

            while (results.Read())
            {
                categories.Add(new Category(
                    results.GetInt32(0), 
                    results.GetString(1), 
                    (Category.CategoryType)results.GetInt32(2)));
            }

            return categories;
        }
    }
}


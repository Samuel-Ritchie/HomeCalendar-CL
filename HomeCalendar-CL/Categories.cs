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
        /// Sets up a database connection with SQLite, if the connection is not null sets categories
        /// to defaults values and types to defaults.
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
        /// <summary>
        /// When a new instance of Categories is created, this method is called to create Category types
        /// into the DB under the table categoryType, columns:
        /// CategoryTypeId = {(int)typesToAdd[type]}
        /// Description = {typesToAdd[type]}
        /// Category types are stored in a list that is looped through Inserting one at a time.
        /// </summary>
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
        /// Adds a new category into the Categories table.
        /// Takes in description and type parameters that act as each respective column.
        /// </summary>
        /// <param name="desc">Description of the category, will be added into Description column.</param>
        /// <param name="type">Type of the category, will be added into CategoryType column.</param>
        public void Add(String desc, Category.CategoryType type)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO categories (Description ,TypeId) " +
                "VALUES(@desc, @typeId);", Connection);

            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@typeId", (int)type);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates a categories values based on ID parameter.
        /// Update statement is done using parameter binding to avoid SQL injection.
        /// </summary>
        /// <param name="id">Category ID to determine which Category to update.</param>
        /// <param name="desc">New description to update previous value.</param>
        /// <param name="categoryType">New categoryType updating the typeId.</param>
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
        /// Deletes a category based on parameter id.
        /// </summary>
        /// <param name="id">Id of category to be deleted.</param>
        public void Delete(int id)
        {
            SQLiteCommand cmd = new SQLiteCommand($"DELETE FROM categories WHERE Id = @id;", Connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Selects all information from categories, loops through the select statement foreach category
        /// and adds each one to list Categories. 
        /// </summary>
        /// <returns>Returns list of all Categories.</returns>
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


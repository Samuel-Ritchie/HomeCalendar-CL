using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Data.SQLite;
using static System.Data.Entity.Infrastructure.Design.Executor;

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
        private string? _FileName;
        private string? _DirName;

        // ====================================================================
        // Properties
        // ====================================================================
        /// <summary>
        /// Gets name of Categories File.
        /// </summary>
        public String? FileName { get { return _FileName; } }
        /// <summary>
        /// Gets name of directory where the Categories file is located.
        /// </summary>
        public String? DirName { get { return _DirName; } }

        // ====================================================================
        // Constructor
        // ====================================================================
        /// <summary>
        /// Sets the Categories List in the Categories Class with default Categories.
        /// </summary>
        public Categories(SQLiteConnection connection = null, bool isnewDb = false)
        {
            if (connection != null && isnewDb)
                SetCategoriesFromDB(connection);
            else if (connection == null)
                SetCategoriesToDefaults();
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
            SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM categories WHERE Id = {id}", Database.dbConnection);
            SQLiteDataReader result = cmd.ExecuteReader();
            
            Category? c = new Category(id, Convert.ToString(result["Description"]), (Category.CategoryType)Convert.ToInt32(result["TypeId"]));
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
            SQLiteCommand cmd = new SQLiteCommand($"DELETE FROM categories", Database.dbConnection);
            cmd.ExecuteNonQuery();

            //Add("School", Category.CategoryType.Event);
            //Add("Personal", Category.CategoryType.Event);
            //Add("VideoGames", Category.CategoryType.Event);
            //Add("Medical", Category.CategoryType.Event);
            //Add("Sleep", Category.CategoryType.Event);
            //Add("Vacation", Category.CategoryType.AllDayEvent);
            //Add("Travel days", Category.CategoryType.AllDayEvent);
            //Add("Canadian Holidays", Category.CategoryType.Holiday);
            //Add("US Holidays", Category.CategoryType.Holiday);
        }

        private void SetCategoriesFromDB(SQLiteConnection connection)
        {
            string query = "SELECT Id, Description, TypeId FROM categories";
            using var cmd = new SQLiteCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string description = reader.GetString(1);
                Category.CategoryType categoryType = (Category.CategoryType)reader.GetInt32(2);


                //Add(description, categoryType);
            }
            //var cmd = new SQLiteCommand("SELECT Id, Description, TypeId FROM categories", connection);
            //using SQLiteDataReader categoriesToAdd = cmd.ExecuteReader();

            //while (categoriesToAdd.Read())
            //{
            //    Add(categoriesToAdd.GetString(1), Category.CategoryType.Event);
            //}
        }

        // ====================================================================
        // Add category
        // ====================================================================
        private void Add(Category category)
        {
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO categories (Id, Description ,TypeId) " +
                $"VALUES({category.Id}, '{category.Description}', {(int)category.Type});", Database.dbConnection);

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
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO categories (Description ,TypeId) " +
                $"VALUES(@desc, {(int)type})", Database.dbConnection);
            cmd.Parameters.AddWithValue("@desc", desc);

            cmd.ExecuteNonQuery();
            string a = "";
        }

        public void UpdateProperties(int id, string desc, Category.CategoryType categoryType)
        {
            //UPDATE table
            //SET column_1 = new_value_1,
            //column_2 = new_value_2
            //WHERE
            //search_condition
            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE categories " +
               $"Description = '{desc}', TypeId = {(int)categoryType}" +
               $"WHERE Id = {id}", Database.dbConnection);

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
            SQLiteCommand cmd = new SQLiteCommand($"DELETE FROM categories WHERE Id = {id}", Database.dbConnection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets a list of all Category Objects in the Categories Class list.
        /// </summary>
        /// <returns>A list of all Category Objects in the Categories Class list</returns>
        public List<Category> List()
        {
            List<Category> categories = new List<Category>();

            SQLiteCommand cmd = new SQLiteCommand($"SELECT Id, Description, TypeId FROM categories", Database.dbConnection);
            SQLiteDataReader results = cmd.ExecuteReader();
            while (results.Read())
            {
                categories.Add(new Category(Convert.ToInt32(results["Id"]), Convert.ToString(results["Description"]), (Category.CategoryType)Convert.ToInt32(results["TypeId"])));
            }

            return categories;
        }
    }
}


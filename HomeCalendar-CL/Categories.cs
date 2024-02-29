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
            {
                SetCategoryTypes();
                SetCategoriesFromDB(connection);
            }
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
            // CategoryTypes
            SQLiteCommand cmd = new SQLiteCommand($"SELECT Description, TypeId FROM categories WHERE Id = {id};", Database.dbConnection);
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
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM categories;", Database.dbConnection);
            cmd.ExecuteNonQuery();

            Add("School", Category.CategoryType.Event);
            Add("Personal", Category.CategoryType.Event);
            Add("VideoGames", Category.CategoryType.Event);
            Add("Medical", Category.CategoryType.Event);
            Add("Sleep", Category.CategoryType.Event);
            Add("Vacation", Category.CategoryType.AllDayEvent);
            Add("Travel days", Category.CategoryType.AllDayEvent);
            Add("Canadian Holidays", Category.CategoryType.Holiday);
            Add("US Holidays", Category.CategoryType.Holiday);
        }

        private void SetCategoryTypes()
        {
            SQLiteCommand cmd = new SQLiteCommand(Database.dbConnection);

            cmd.CommandText =
                "DELETE FROM categoryTypes;" +
                "INSERT INTO categoryTypes (Id ,Description) " +
                    $"VALUES({(int)Category.CategoryType.Holiday}, '{Category.CategoryType.Holiday}');" + 
                "INSERT INTO categoryTypes (Id ,Description) " +
                    $"VALUES({(int)Category.CategoryType.Availability}, '{Category.CategoryType.Availability}');" +
                "INSERT INTO categoryTypes (Id ,Description) " +
                    $"VALUES({(int)Category.CategoryType.Event}, '{Category.CategoryType.Event}');" +
                "INSERT INTO categoryTypes (Id ,Description) " +
                    $"VALUES({(int)Category.CategoryType.AllDayEvent}, '{Category.CategoryType.AllDayEvent}');";

            cmd.ExecuteNonQuery();
        }

        private void SetCategoriesFromDB(SQLiteConnection connection)
        {
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM categories;", Database.dbConnection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("SELECT Id, Description, TypeId FROM categories;", connection);
            using SQLiteDataReader categoriesToAdd = cmd.ExecuteReader();

            if (categoriesToAdd.HasRows)
            {
                while (categoriesToAdd.Read())
                {
                    Add(new Category(
                        categoriesToAdd.GetInt32(0),
                        categoriesToAdd.GetString(1),
                        (Category.CategoryType)categoriesToAdd.GetInt32(2)));
                }
            }
            else
                SetCategoriesToDefaults();
        }

        // ====================================================================
        // Add category
        // ====================================================================
        private void Add(Category category)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                $"INSERT INTO categories (Id, Description ,TypeId) " +
                $"VALUES(@id, @desc, @typeId)", Database.dbConnection);

            cmd.Parameters.AddWithValue("@id", category.Id);
            cmd.Parameters.AddWithValue("@desc", category.Description);
            cmd.Parameters.AddWithValue("@typeId", (int)category.Type);

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
                "VALUES(@desc, @typeId)", Database.dbConnection);

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
                "WHERE Id = @id;", Database.dbConnection);

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

            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Description, TypeId FROM categories;", Database.dbConnection);
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


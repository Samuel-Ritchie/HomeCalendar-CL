using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Data.SQLite;

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
        private static String DefaultFileName = "calendarCategories.txt";
        private List<Category> _Categories = new List<Category>();
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
            else if (connection != null && !isnewDb)
                SetCategoriesFromDB(connection);
            else
                SetCategoriesToDefaults();
        }

        // ====================================================================
        // Methods
        // ====================================================================
        /// <summary>
        /// Gets the Category according to it's Id.
        /// Uses the Id passed as a parameter to select and return a specific Category.
        /// </summary>
        /// <param name="i">The Id number of the Category Object.</param>
        /// <returns>A Category Object with the Id passed.</returns>
        /// <exception cref="Exception">If Id does not correspond to an existing Category.</exception>
        public Category GetCategoryFromId(int i)
        {
            Category? c = _Categories.Find(x => x.Id == i);
            if (c == null)
            {
                throw new Exception("Cannot find category with id " + i.ToString());
            }
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
            _Categories.Clear();

            // ---------------------------------------------------------------
            // Add Defaults
            // ---------------------------------------------------------------
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

        public void SetCategoriesFromDB(SQLiteConnection connection)
        {
            var cmd = new SQLiteCommand("Select * from categories", connection);
            SQLiteDataReader categoriesToAdd = cmd.ExecuteReader();
            while (categoriesToAdd.Read())
            {
                //_Categories.Add();
                Add(Convert.ToString(categoriesToAdd["Description"]), Category.CategoryType.Event);
            }
        }

        // ====================================================================
        // Add category
        // ====================================================================
        private void Add(Category category)
        {
            _Categories.Add(category);
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
            int new_num = 1;
            if (_Categories.Count > 0)
            {
                new_num = (from c in _Categories select c.Id).Max();
                new_num++;
            }
            _Categories.Add(new Category(new_num, desc, type));
        }

        public void UpdateProperties(int id, string desc, Category.CategoryType categoryType)
        {
            _Categories[_Categories.FindIndex(x => x.Id == id)] = new Category(id, desc, categoryType);
        }

        /// <summary>
        /// Removes an existing Category Object from the Categories List in the Categories Class.
        /// Takes in an ID number that determines which event to remove.
        /// </summary>
        /// <param name="Id">(Int) The id of the Category Object to Remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the Id passed is not in the existing range of the list.</exception>
        public void Delete(int Id)
        {
            try
            {
                int i = _Categories.FindIndex(x => x.Id == Id);
                _Categories.RemoveAt(i);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of all Category Objects in the Categories Class list.
        /// </summary>
        /// <returns>A list of all Category Objects in the Categories Class list</returns>
        public List<Category> List()
        {
            List<Category> newList = new List<Category>();
            foreach (Category category in _Categories)
            {
                newList.Add(new Category(category));
            }
            return newList;
        }
    }
}


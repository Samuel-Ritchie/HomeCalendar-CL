using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

// ============================================================================
// (c) Sandy Bultena 2018
// * Released under the GNU General Public License
// ============================================================================

namespace Calendar
{
    // ====================================================================
    // CLASS: categories
    //        - A collection of category items,
    //        - Read / write to file
    //        - etc
    // ====================================================================
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
        public Categories()
        {
            SetCategoriesToDefaults();
        }

        // ====================================================================
        // get a specific category from the list where the id is the one specified
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

        // ====================================================================
        // populate categories from a file
        // if filepath is not specified, read/save in AppData file
        // Throws System.IO.FileNotFoundException if file does not exist
        // Throws System.Exception if cannot read the file correctly (parsing XML)
        // ====================================================================
        /// <summary>
        /// Sets/Updates the list of Category Objects using default values, or data from a Categories file.
        /// Clears the old list of Category Objects, along with the file name and path.
        /// Tests reading from file. Throws exception if file not found.
        /// Parses the XML from the file, storing values in an Categories Objects.
        /// Adds each of the created Category Objects to the Categories list data field.
        /// re-Assigns file Names and Paths for future use.
        /// </summary>
        /// <param name="filepath">Name of file to load.</param>
        /// <exception cref="System.IO.FileNotFoundException">If file doesn't exist.</exception>
        /// <exception cref="System.Exception">If XML Parsing fails.</exception>
        public void ReadFromFile(String? filepath = null)
        {

            // ---------------------------------------------------------------
            // reading from file resets all the current categories,
            // ---------------------------------------------------------------
            _Categories.Clear();

            // ---------------------------------------------------------------
            // reset default dir/filename to null 
            // ... filepath may not be valid, 
            // ---------------------------------------------------------------
            _DirName = null;
            _FileName = null;

            // ---------------------------------------------------------------
            // get filepath name (throws exception if it doesn't exist)
            // ---------------------------------------------------------------
            filepath = CalendarFiles.VerifyReadFromFileName(filepath, DefaultFileName);

            // ---------------------------------------------------------------
            // If file exists, read it
            // ---------------------------------------------------------------
            _ReadXMLFile(filepath);
            _DirName = Path.GetDirectoryName(filepath);
            _FileName = Path.GetFileName(filepath);
        }

        // ====================================================================
        // save to a file
        // if filepath is not specified, read/save in AppData file
        // ====================================================================
        /// <summary>
        /// Saves Categories list of Category Objects data to a file in the XML Format.
        /// Creates a file name and chooses a path if the values are previously null.
        /// Tests writing to file. Throws exception if not found.
        /// Saves data from Category Objects in Categories Class in as XML file.
        /// re-Assigns file Names and Paths for future use.
        /// </summary>
        /// <param name="filepath">Name of file to write to.</param>
        /// <exception cref="Exception">If failed to write to file.</exception>
        public void SaveToFile(String? filepath = null)
        {
            // ---------------------------------------------------------------
            // if file path not specified, set to last read file
            // ---------------------------------------------------------------
            if (filepath == null && DirName != null && FileName != null)
            {
                filepath = DirName + "\\" + FileName;
            }

            // ---------------------------------------------------------------
            // just in case filepath doesn't exist, reset path info
            // ---------------------------------------------------------------
            _DirName = null;
            _FileName = null;

            // ---------------------------------------------------------------
            // get filepath name (throws exception if it doesn't exist)
            // ---------------------------------------------------------------
            filepath = CalendarFiles.VerifyWriteToFileName(filepath, DefaultFileName);

            // ---------------------------------------------------------------
            // save as XML
            // ---------------------------------------------------------------
            _WriteXMLFile(filepath);

            // ----------------------------------------------------------------
            // save filename info for later use
            // ----------------------------------------------------------------
            _DirName = Path.GetDirectoryName(filepath);
            _FileName = Path.GetFileName(filepath);
        }

        // ====================================================================
        // set categories to default
        // ====================================================================
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

        // ====================================================================
        // Delete category
        // ====================================================================
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

        // ====================================================================
        // Return list of categories
        // Note:  make new copy of list, so user cannot modify what is part of
        //        this instance
        // ====================================================================
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

        // ====================================================================
        // read from an XML file and add categories to our categories list
        // ====================================================================
        private void _ReadXMLFile(String filepath)
        {

            // ---------------------------------------------------------------
            // read the categories from the xml file, and add to this instance
            // ---------------------------------------------------------------
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);

                foreach (XmlNode category in doc.DocumentElement.ChildNodes)
                {
                    String id = (((XmlElement)category).GetAttributeNode("ID")).InnerText;
                    String typestring = (((XmlElement)category).GetAttributeNode("type")).InnerText;
                    String desc = ((XmlElement)category).InnerText;

                    Category.CategoryType type;
                    switch (typestring.ToLower())
                    {
                        case "event":
                            type = Category.CategoryType.Event;
                            break;
                        case "allday":
                            type = Category.CategoryType.AllDayEvent;
                            break;
                        case "holiday":
                            type = Category.CategoryType.Holiday;
                            break;
                        default:
                            type = Category.CategoryType.Event;
                            break;
                    }
                    this.Add(new Category(int.Parse(id), desc, type));
                }

            }
            catch (Exception e)
            {
                throw new Exception("ReadXMLFile: Reading XML " + e.Message);
            }

        }


        // ====================================================================
        // write all categories in our list to XML file
        // ====================================================================
        private void _WriteXMLFile(String filepath)
        {
            try
            {
                // create top level element of categories
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<Categories></Categories>");

                // foreach Category, create an new xml element
                foreach (Category cat in _Categories)
                {
                    XmlElement ele = doc.CreateElement("Category");
                    XmlAttribute attr = doc.CreateAttribute("ID");
                    attr.Value = cat.Id.ToString();
                    ele.SetAttributeNode(attr);
                    XmlAttribute type = doc.CreateAttribute("type");
                    type.Value = cat.Type.ToString();
                    ele.SetAttributeNode(type);

                    XmlText text = doc.CreateTextNode(cat.Description);
                    doc.DocumentElement.AppendChild(ele);
                    doc.DocumentElement.LastChild.AppendChild(text);

                }

                // write the xml to FilePath
                doc.Save(filepath);

            }
            catch (Exception e)
            {
                throw new Exception("_WriteXMLFile: Reading XML " + e.Message);
            }

        }

    }
}


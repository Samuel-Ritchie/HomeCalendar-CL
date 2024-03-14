using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ============================================================================
// (c) Sandy Bultena 2018
// * Released under the GNU General Public License
// ============================================================================


namespace Calendar
{
    // ========================================================================
    // CLASS: HomeCalendar
    //        - Combines a Categories Class and an Events Class
    //        - One File defines Category and Events File
    // ========================================================================
    /// <summary>
    /// Holds Categories and Events Objects created by default or loaded from a Calendar File.
    /// Holds Calendar Categories and Events Objects, which are either generated from the default assortments, or from a Calendar file.
    /// The Categories Object Field holds a list of Category Objects.
    /// The Events Object Field holds a list of Event Objects.
    /// </summary>
    public class HomeCalendar
    {
        private string? _FileName;
        private string? _DirName;
        private Categories _categories;
        private Events _events;

        // ====================================================================
        // Properties
        // ===================================================================

        // Properties (categories and events object)
        /// <summary>
        /// Gets the Calendar Catagories object. *** The Categories Object does. ***
        /// </summary>
        /// <value>
        /// (Categories) Object that holds Categories info.
        /// </value>
        public Categories categories { get { return _categories; } }
        /// <summary>
        /// Gets the Calendar Events Object. *** The Events Object does. ***
        /// </summary>
        /// /// <value>
        /// (Events) Object that holds Events info.
        /// </value>
        public Events events { get { return _events; } }

        // -------------------------------------------------------------------
        // Constructor (new... default categories, no events)
        // -------------------------------------------------------------------
        /// <summary>
        /// Creates a new HomeCalendar Object.
        /// Default values are given to the Categories Object, and there will be no Events created in the Events Object.
        /// </summary>
        /// <value>
        /// (HomeCalendar) new Object. Object data fields set to default.
        /// </value>
        public HomeCalendar(String databaseFile, String eventsXMLFile, bool newDB = false)
        {
            // if database exists, and user doesn't want a new database, open existing DB
            if (!newDB && File.Exists(databaseFile))
            {
                Database.existingDatabase(databaseFile);
            }

            // file did not exist, or user wants a new database, so open NEW DB
            else
            {
                Database.newDatabase(databaseFile);
                newDB = true;
            }

            // create the category object
            _categories = new Categories(Database.dbConnection, newDB);

            // create the _events course
            _events = new Events();
        }


        #region GetList



        // ============================================================================
        // Get all events list
        // ============================================================================
        /// <summary>
        /// Gets the list of all CalendarItems held in a HomeCalendar Object.
        /// Calculates a date range based on the date parameters passed to it.
        /// Creates a list of CalendarItems inside the time frame.
        /// Can filter out Categories and only work on one if FilterFlag is set to true and a CategoryID is passed.
        /// </summary>
        /// <param name="Start">(DateTime) The date where items start being included in the list.</param>
        /// <param name="End">(DateTime) The date where items stop being included in the list.</param>
        /// <param name="FilterFlag">(Bool) If true, items are included by the CategoryID; Otherwise, items of any CategoryID are included in the list.</param>
        /// <param name="CategoryID">(Int) The only CategoryID that is included in the list if the FilterFlag is set to true.</param>
        /// <returns>(List<CalendarItem>) A list of CalendarItem Objects.</returns>
        /// <exception cref="ArgumentNullException">If no Start DateTime is passed to function.</exception>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///  HomeCalendar MainCalendar = new HomeCalendar();
        ///  MainCalendar.ReadFromFile(filename);
        /// 
        ///   List <CalendarItem> calendarItems = calendar.GetCalendarItems(startDate, endDate, false, 0);
        ///             
        ///   // print important information
        ///   foreach (var ci in calendarItems)
        ///   {
        ///     Console.WriteLine(
        ///        String.Format("{0} {1,-20}  {2,8} {3,12}",
        ///            ci.StartDateTime.ToString("yyyy/MMM/dd/HH/mm"),
        ///            ci.ShortDescription,
        ///            ci.DurationInMinutes, ci.BusyTime)
        ///      ); ;
        ///   }
        ///
        ///
        /// ]]>
        /// </code>
        /// </example>
        public List<CalendarItem> GetCalendarItems(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
        {
            // ------------------------------------------------------------------------
            // return joined list within time frame
            // ------------------------------------------------------------------------
            Start = Start ?? new DateTime(1900, 1, 1);
            End = End ?? new DateTime(2500, 1, 1);

            var query =  from c in _categories.List()
                        join e in _events.List() on c.Id equals e.Category
                        where e.StartDateTime >= Start && e.StartDateTime <= End
                        select new { CatId = c.Id, EventId = e.Id, e.StartDateTime, Category = c.Description, e.Details, e.DurationInMinutes };

            // ------------------------------------------------------------------------
            // create a CalendarItem list with totals,
            // ------------------------------------------------------------------------
            List<CalendarItem> items = new List<CalendarItem>();
            Double totalBusyTime = 0;

            foreach (var e in query.OrderBy(q => q.StartDateTime))
            {
                // filter out unwanted categories if filter flag is on
                if (FilterFlag && CategoryID != e.CatId)
                {
                    continue;
                }

                // keep track of running totals
                totalBusyTime = totalBusyTime + e.DurationInMinutes;
                items.Add(new CalendarItem
                {
                    CategoryID = e.CatId,
                    EventID = e.EventId,
                    ShortDescription = e.Details,
                    StartDateTime = e.StartDateTime,
                    DurationInMinutes = e.DurationInMinutes,
                    Category = e.Category,
                    BusyTime = totalBusyTime
                });
            }

            return items;
        }

        // ============================================================================
        // Group all events month by month (sorted by year/month)
        // returns a list of CalendarItemsByMonth which is 
        // "year/month", list of calendar items, and totalBusyTime for that month
        // ============================================================================
        /// <summary>
        /// Returns a list of Calendar Items. The list has the option to be filtered by Category, and which time the Items takes place.
        /// </summary>
        /// <param name="Start">(DateTime) The date where items start being included in the list.</param>
        /// <param name="End">(DateTime) The date where items stop being included in the list.</param>
        /// <param name="FilterFlag">(Bool) If true, items are included by CategoryID; Otherwise, items of any CategoryID are included in the list.</param>
        /// <param name="CategoryID">(Int) The only CategoryID that is included in the list if the FilterFlag is set to true.</param>
        /// <returns>A list of Calendar Items.</returns>
        /// <exception cref="ArgumentNullException">If no Start DateTime is passed to function.</exception>
        public List<CalendarItemsByMonth> GetCalendarItemsByMonth(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
        {
            // -----------------------------------------------------------------------
            // get all items first
            // -----------------------------------------------------------------------
            List<CalendarItem> items = GetCalendarItems(Start, End, FilterFlag, CategoryID);

            // -----------------------------------------------------------------------
            // Group by year/month
            // -----------------------------------------------------------------------
            var GroupedByMonth = items.GroupBy(c => c.StartDateTime.Year.ToString("D4") + "/" + c.StartDateTime.Month.ToString("D2"));

            // -----------------------------------------------------------------------
            // create new list
            // -----------------------------------------------------------------------
            var summary = new List<CalendarItemsByMonth>();
            foreach (var MonthGroup in GroupedByMonth)
            {
                // calculate totalBusyTime for this month, and create list of items
                double total = 0;
                var itemsList = new List<CalendarItem>();
                foreach (var item in MonthGroup)
                {
                    total = total + item.DurationInMinutes;
                    itemsList.Add(item);
                }

                // Add new CalendarItemsByMonth to our list
                summary.Add(new CalendarItemsByMonth
                {
                    Month = MonthGroup.Key,
                    Items = itemsList,
                    TotalBusyTime = total
                });
            }

            return summary;
        }

        // ============================================================================
        // Group all events by category (ordered by category name)
        // ============================================================================
        /// <summary>
        /// Returns a list of CalendarItemsByCategory Objects.
        /// Uses GetCalendarItems() to get a list of the items in the filtered time frame and Category if specified.
        /// Divides the retrieved list by Category, putting each grouped list into a CalendarItemsByCategory Object.
        /// Also stores the name of the Category and the lists total busy time according to each item list in the Object.
        /// Creates a list that holds all of the CalendarItemsByCategory Objects and returns it.
        /// </summary>
        /// <param name="Start">(DateTime) The date where items start being included in the base items list.</param>
        /// <param name="End">(DateTime) The date where items stop being included in the base items list.</param>
        /// <param name="FilterFlag">(Bool) If true, items are included by CategoryID; Otherwise, items of any CategoryID are included in the list.</param>
        /// <param name="CategoryID">(Int) The only CategoryID that is included in the list if the FilterFlag is set to true.</param>
        /// <returns>A list of CalendarItemsByCategory Objects.</returns>
        /// <exception cref="ArgumentNullException">If no Start DateTime is passed to function.</exception>
        public List<CalendarItemsByCategory> GetCalendarItemsByCategory(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
        {
            // -----------------------------------------------------------------------
            // get all items first
            // -----------------------------------------------------------------------
            List<CalendarItem> filteredItems = GetCalendarItems(Start, End, FilterFlag, CategoryID);

            // -----------------------------------------------------------------------
            // Group by Category
            // -----------------------------------------------------------------------
            var GroupedByCategory = filteredItems.GroupBy(c => c.Category);

            // -----------------------------------------------------------------------
            // create new list
            // -----------------------------------------------------------------------
            var summary = new List<CalendarItemsByCategory>();
            foreach (var CategoryGroup in GroupedByCategory.OrderBy(g => g.Key))
            {
                // calculate totalBusyTime for this category, and create list of items
                double total = 0;
                var items = new List<CalendarItem>();
                foreach (var item in CategoryGroup)
                {
                    total = total + item.DurationInMinutes;
                    items.Add(item);
                }

                // Add new CalendarItemsByCategory to our list
                summary.Add(new CalendarItemsByCategory
                {
                    Category = CategoryGroup.Key,
                    Items = items,
                    TotalBusyTime = total
                });
            }

            return summary;
        }



        // ============================================================================
        // Group all events by category and Month
        // creates a list of Dictionary objects with:
        //          one dictionary object per month,
        //          and one dictionary object for the category total busy times
        // 
        // Each per month dictionary object has the following key value pairs:
        //           "Month", <name of month>
        //           "TotalBusyTime", <the total durations for the month>
        //             for each category for which there is an event in the month:
        //             "items:category", a List<CalendarItem>
        //             "category", the total busy time for that category for this month
        // The one dictionary for the category total busy times has the following key value pairs:
        //             for each category for which there is an event in ANY month:
        //             "category", the total busy time for that category for all the months
        // ============================================================================
        /// <summary>
        /// Returns a List of Dictionaries that correspond to the items in each month of the year.
        /// Uses GetCalendarItemsByMonth to get a list of Items for each month in the year.
        /// Loops over each months objects, sorting their individual items by Category.
        /// Saves all relevant information for each month in a dictionary, adding the dictionary to the list that holds all of them.
        /// Relevant information includes: Month Name, Total busy time, a list of details for each item in the month.
        /// Returns the list of dictionaries.
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="FilterFlag"></param>
        /// <param name="CategoryID"></param>
        /// <returns>A list of dictionaries, each corresponding to a month, and Categories, and the Items of that Category.</returns>
        /// <exception cref="ArgumentNullException">If no Start DateTime is passed to function.</exception>
        public List<Dictionary<string,object>> GetCalendarDictionaryByCategoryAndMonth(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
        {
            // -----------------------------------------------------------------------
            // get all items by month 
            // -----------------------------------------------------------------------
            List<CalendarItemsByMonth> GroupedByMonth = GetCalendarItemsByMonth(Start, End, FilterFlag, CategoryID);

            // -----------------------------------------------------------------------
            // loop over each month
            // -----------------------------------------------------------------------
            var summary = new List<Dictionary<string, object>>();
            var totalBusyTimePerCategory = new Dictionary<String, Double>();

            foreach (var MonthGroup in GroupedByMonth)
            {
                // create record object for this month
                Dictionary<string, object> record = new Dictionary<string, object>();
                record["Month"] = MonthGroup.Month;
                record["TotalBusyTime"] = MonthGroup.TotalBusyTime;

                // break up the month items into categories
                var GroupedByCategory = MonthGroup.Items.GroupBy(c => c.Category);

                // -----------------------------------------------------------------------
                // loop over each category
                // -----------------------------------------------------------------------
                foreach (var CategoryGroup in GroupedByCategory.OrderBy(g => g.Key))
                {

                    // calculate totals for the cat/month, and create list of items
                    double totalCategoryBusyTimeForThisMonth = 0;
                    var details = new List<CalendarItem>();

                    foreach (var item in CategoryGroup)
                    {
                        totalCategoryBusyTimeForThisMonth = totalCategoryBusyTimeForThisMonth + item.DurationInMinutes;
                        details.Add(item);
                    }

                    // add new properties and values to our record object
                    record["items:" + CategoryGroup.Key] = details;
                    record[CategoryGroup.Key] = totalCategoryBusyTimeForThisMonth;

                    // keep track of totals for each category
                    if (totalBusyTimePerCategory.TryGetValue(CategoryGroup.Key, out Double currentTotalBusyTimeForCategory))
                    {
                        totalBusyTimePerCategory[CategoryGroup.Key] = currentTotalBusyTimeForCategory + totalCategoryBusyTimeForThisMonth;
                    }
                    else
                    {
                        totalBusyTimePerCategory[CategoryGroup.Key] = totalCategoryBusyTimeForThisMonth;
                    }
                }

                // add record to collection
                summary.Add(record);
            }
            // ---------------------------------------------------------------------------
            // add final record which is the totals for each category
            // ---------------------------------------------------------------------------
            Dictionary<string, object> totalsRecord = new Dictionary<string, object>();
            totalsRecord["Month"] = "TOTALS";

            foreach (var cat in categories.List())
            {
                try
                {
                    totalsRecord.Add(cat.Description, totalBusyTimePerCategory[cat.Description]);
                }
                catch { }
            }
            summary.Add(totalsRecord);


            return summary;
        }




        #endregion GetList

    }
}

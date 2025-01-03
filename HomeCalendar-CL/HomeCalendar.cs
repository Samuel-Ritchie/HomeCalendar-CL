﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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
        public HomeCalendar(String databaseFile, bool newDB = false)
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
            // _events = new Events(Database.dbConnection, newDB);
            _events = new Events(Database.dbConnection, newDB);
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

            DateTime realStart = Start ?? new DateTime(1900, 1, 1);
            DateTime realEnd = End ?? new DateTime(2500, 1, 1);

            SQLiteCommand categoryCmd = new SQLiteCommand(
                $"SELECT E.Id, E.StartDateTime, E.Details, E.DurationInMinutes, C.Id, C.Description, C.TypeId " + 
                    $"FROM events as E INNER JOIN categories as C ON E.CategoryId == C.Id " +
                    $"WHERE E.StartDateTime >= '{realStart}' AND E.StartDateTime <= '{realEnd}';", Database.dbConnection);

            SQLiteDataReader categoryResult = categoryCmd.ExecuteReader();

            // Create list to hold all rows.
            List<CalendarItem> items = new List<CalendarItem>();
            Double totalBusyTime = 0;

            /*
            // Event ID:                 0
            // Event StartDateTime:      1
            // Event Details:            2
            // Event DurationInMinutes:  3

            // Category ID:              4
            // Category Description:     5
            // Category TypeID:          6
            */

            while (categoryResult.Read())
            {
                if (FilterFlag && CategoryID != categoryResult.GetInt32(4))
                {
                    continue;
                }

                totalBusyTime = totalBusyTime + categoryResult.GetDouble(3);
                items.Add(new CalendarItem
                {
                    CategoryID = categoryResult.GetInt32(4),
                    EventID = categoryResult.GetInt32(0),
                    ShortDescription = categoryResult.GetString(2),
                    StartDateTime = categoryResult.GetDateTime(1),
                    DurationInMinutes = categoryResult.GetDouble(3),
                    Category = categoryResult.GetString(5),
                    BusyTime = totalBusyTime
                });
            }

            return items;
        }

        /// <summary>
        /// Returns a list of Calendar Items. The list has the option to be filtered by Category, and which time the Items takes place.
        /// </summary>
        /// <param name="Start">(DateTime) The date where items start being included in the list.</param>
        /// <param name="End">(DateTime) The date where items stop being included in the list.</param>
        /// <param name="FilterFlag">(Bool) If true, items are included by CategoryID; Otherwise, items of any CategoryID are included in the list.</param>
        /// <param name="CategoryID">(Int) The only CategoryID that is included in the list if the FilterFlag is set to true.</param>
        /// <returns>A list of Calendar Items.</returns>
        public List<CalendarItemsByMonth> GetCalendarItemsByMonth(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
        {
            // Default times if null
            DateTime realStart = Start ?? new DateTime(1900, 1, 1);
            DateTime realEnd = End ?? new DateTime(2500, 1, 1);

            // Filtering if needed in query
            SQLiteCommand cmd = (FilterFlag) ? 
                new SQLiteCommand(
                    $"SELECT E.Id, E.StartDateTime, E.Details, E.DurationInMinutes, C.Id, C.Description, C.TypeId " +
                    $"FROM events as E INNER JOIN categories as C ON E.CategoryId == C.Id " +
                    $"WHERE E.StartDateTime >= @start AND E.StartDateTime <= @end AND e.CategoryId == @categoryIdFlag " +
                    $"ORDER BY E.StartDateTime;", Database.dbConnection) 
                :
                new SQLiteCommand(
                    $"SELECT E.Id, E.StartDateTime, E.Details, E.DurationInMinutes, C.Id, C.Description, C.TypeId " +
                    $"FROM events as E INNER JOIN categories as C ON E.CategoryId == C.Id " +
                    $"WHERE E.StartDateTime >= @start AND E.StartDateTime <= @end " +
                    $"ORDER BY E.StartDateTime;", Database.dbConnection);

            cmd.Parameters.AddWithValue("@categoryIdFlag", CategoryID);
            cmd.Parameters.AddWithValue("@start", realStart.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@end", realEnd.ToString("yyyy-MM-dd HH:mm:ss"));

            SQLiteDataReader result = cmd.ExecuteReader();

            // Create list to hold all rows.
            List<CalendarItemsByMonth> items = new List<CalendarItemsByMonth>();

            /*
            // Event ID:                 0
            // Event StartDateTime:      1
            // Event Details:            2
            // Event DurationInMinutes:  3

            // Category ID:              4
            // Category Description:     5
            // Category TypeID:          6
            */
            CalendarItemsByMonth month = new CalendarItemsByMonth();
            const int MONTH_INDEX = 7;
            bool isFirstRow = true;

            // Reading all rows
            while (result.Read())
            {
                // Changing CalendarItemsByMonth object if different month and year
                if (month.Month != result.GetString(1).Substring(0, MONTH_INDEX) || isFirstRow)
                {
                    if (!isFirstRow) items.Add(month);

                    month = new CalendarItemsByMonth
                    {
                        Month = result.GetString(1).Substring(0, MONTH_INDEX),
                        Items = new List<CalendarItem>(),
                        TotalBusyTime = 0
                    };
                    isFirstRow = false;
                }

                // Adding to total busy time
                month.TotalBusyTime += result.GetDouble(3);

                // Adding to list of CalendarItems in CalendarItemsByMonth
                month.Items.Add(new CalendarItem
                {
                    CategoryID = result.GetInt32(4),
                    EventID = result.GetInt32(0),
                    ShortDescription = result.GetString(2),
                    StartDateTime = result.GetDateTime(1),
                    DurationInMinutes = result.GetDouble(3),
                    Category = result.GetString(5),
                });

            }

            // Adding last CalendarItemsByMonth if any rows were read
            if (!isFirstRow)
                items.Add(month);

            return items;
        }

        /// <summary>
        /// Retrieves a list of calendar items grouped by category, optionally filtered by date range and category ID.
        /// </summary>
        /// <param name="Start">The start date of the date range to filter calendar items. If null, defaults to January 1, 1900.</param>
        /// <param name="End">The end date of the date range to filter calendar items. If null, defaults to January 1, 2500.</param>
        /// <param name="FilterFlag">A boolean flag indicating whether to filter calendar items by the specified category ID.</param>
        /// <param name="CategoryID">The category ID to filter calendar items by. Only used if FilterFlag is true.</param>
        /// <returns>A list of CalendarItemsByCategory objects, each representing a category with its associated calendar items and total busy time.</returns>
        public List<CalendarItemsByCategory> GetCalendarItemsByCategory(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
        {
            // Default times if null
            DateTime realStart = Start ?? new DateTime(1900, 1, 1);
            DateTime realEnd = End ?? new DateTime(2500, 1, 1);

            // Filtering if needed in query
            SQLiteCommand cmd = (FilterFlag) ?
                new SQLiteCommand(
                    $"SELECT E.Id, E.StartDateTime, E.Details, E.DurationInMinutes, C.Id, C.Description, C.TypeId " +
                    $"FROM events as E INNER JOIN categories as C ON E.CategoryId == C.Id " +
                    $"WHERE E.StartDateTime >= @start AND E.StartDateTime <= @end AND e.CategoryId == @categoryIdFlag " +
                    $"ORDER BY C.Description, E.DurationInMinutes DESC;", Database.dbConnection)
                :
                new SQLiteCommand(
                    $"SELECT E.Id, E.StartDateTime, E.Details, E.DurationInMinutes, C.Id, C.Description, C.TypeId " +
                    $"FROM events as E INNER JOIN categories as C ON E.CategoryId == C.Id " +
                    $"WHERE E.StartDateTime >= @start AND E.StartDateTime <= @end " +
                    $"ORDER BY C.Description, E.DurationInMinutes DESC;", Database.dbConnection);

            cmd.Parameters.AddWithValue("@categoryIdFlag", CategoryID);
            cmd.Parameters.AddWithValue("@start", realStart.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@end", realEnd.ToString("yyyy-MM-dd HH:mm:ss"));

            SQLiteDataReader result = cmd.ExecuteReader();

            // Create list to hold all rows.
            List<CalendarItemsByCategory> items = new List<CalendarItemsByCategory>();

            /*
            // Event ID:                 0
            // Event StartDateTime:      1
            // Event Details:            2
            // Event DurationInMinutes:  3

            // Category ID:              4
            // Category Description:     5
            // Category TypeID:          6
            */
            CalendarItemsByCategory category = new CalendarItemsByCategory();
            bool isFirstRow = true;

            while (result.Read())
            {
                // Changing CalendarItemsByCategory object if different month and year
                if (category.Category != result.GetString(5) || isFirstRow)
                {
                    if (!isFirstRow) items.Add(category);

                    category = new CalendarItemsByCategory
                    {
                        Category = result.GetString(5),
                        Items = new List<CalendarItem>(),
                        TotalBusyTime = 0
                    };
                    isFirstRow = false;
                }

                // Adding to total busy time
                category.TotalBusyTime += result.GetDouble(3);

                // Adding to list of CalendarItems in CalendarItemsByCategory
                category.Items.Add(new CalendarItem
                {
                    CategoryID = result.GetInt32(4),
                    EventID = result.GetInt32(0),
                    ShortDescription = result.GetString(2),
                    StartDateTime = result.GetDateTime(1),
                    DurationInMinutes = result.GetDouble(3),
                    Category = result.GetString(5),
                });

            }

            // Adding last CalendarItemsByCategory if any rows were read
            if (!isFirstRow)
                items.Add(category);

            return items;
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
        public List<Dictionary<string, object>> GetCalendarDictionaryByCategoryAndMonth(DateTime? Start, DateTime? End, bool FilterFlag, int CategoryID)
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

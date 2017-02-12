using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static partial class Extensions
    {
        #region Methods
        /// <summary>
        /// Get first column of first row
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object firstColumnOfFirstRow(this DataTable dataTable, string fieldName, object defaultValue = null)
        {
            object result;

            if ((0 < dataTable?.Rows.Count) && (0 < dataTable?.Columns.Count))
                result = dataTable.Rows[0][fieldName];
            else
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// Get Array of a feild
        /// </summary>
        /// <returns></returns>
        public static object[] getArray(this SqlDataReader dataReader, string fieldName)
        {
            List<object> result = new List<object>();

            if (null != dataReader)
            {
                while (dataReader.Read())
                    result.Add(dataReader[fieldName]);
                dataReader.Close();
            }

            return result.ToArray();
        }

        /// <summary>
        /// Convert All Gregorian dates to Shamsi date
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable makePersianDate(this DataTable table, string postfix = "_Shamsi", bool dateTime = false)
        {
            /*
				1- Find datetime columns
				2- Add _shamsi columns
				3- Convert dates
			*/

            List<string> cols = new List<string>();

            if ((null != table) && (0 < table.Rows.Count))
            {
                /// I used loop by for syntax because Columns collection does change after adding new column and we have an exeption on it.
                for (int i = 0; i < table.Columns.Count; i++)
                    if (table.Columns[i].DataType == typeof(DateTime))
                    {
                        // Add to list
                        cols.Add(table.Columns[i].ColumnName);

                        // Create a new column
                        table.Columns.Add(table.Columns[i].ColumnName + postfix, typeof(string));
                        table.Columns[table.Columns[i].ColumnName + postfix].SetOrdinal(table.Columns[i].Ordinal + 1);
                    }

                // Convert data
                foreach (DataRow row in table.Rows)
                    foreach (string col in cols)
                        if ((null != row[col]) && (row[col].GetType() != typeof(DBNull)))
                            if (dateTime)
                                row[col + postfix] = DateTime.Parse(row[col].ToString()).toPersianDateTime();
                            else
                                row[col + postfix] = DateTime.Parse(row[col].ToString()).toPersianDate();
            }

            return table;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseDAL.Model;
using Common.Helper;
using System.Drawing;
using System.Data.SqlClient;

namespace System
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Export To Excel
        /// </summary>
        public static void exportToExcel(DataGridView dgvGrid, string sheetName = "Sheet1")
        {
            if (null != dgvGrid.DataSource)
            {
                SaveFileDialog fsd = new SaveFileDialog();

                fsd.Filter = "*.xls|*.xls";
                if (fsd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable dt = ((DataTable)dgvGrid.DataSource).Copy();

                    // Remove Hidden columns
                    int j = 0;

                    for (int i = 0; i < dgvGrid.ColumnCount; i++)
                    {
                        dt.Columns[j].Caption = dgvGrid.Columns[i].HeaderText;
                        dt.Columns[j].ColumnName = dgvGrid.Columns[i].HeaderText;

                        if (!dgvGrid.Columns[i].Visible)
                            dt.Columns.RemoveAt(j);
                        else
                            j++;
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Clear();
                    ds.Tables.Add(dt);

                    ds.WriteXml(fsd.FileName);
                    try
                    {
                        System.Diagnostics.Process.Start(fsd.FileName);
                    }
                    catch (Exception ex)
                    {
                        ///TODO : RAISE ERROR HANDLER
                    }
                }
            }
        }

        /// <summary>
        /// Grid Header & Visible
        /// </summary>
        /// <param name="data"></param>
        public static void gridHeaderSetup(this DataGridView grid, params object[] data)
        {
            if ((null != grid) && (null != data))
            {
                int mCol = (int)Math.Min(data.Length / 2, grid.ColumnCount);

                if (mCol > 1)
                    for (int i = 0; i < mCol; i++)
                    {
                        grid.Columns[i].HeaderText = data[i * 2].ToString();
                        grid.Columns[i].Visible = Convert.ToBoolean(data[i * 2 + 1]);
                    }
            }
        }

        /// <summary>
        /// Calculate total count of a column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static decimal calcTotal(this DataTable table, string field)
        {
            decimal result = 0;

            if (table is DataTable)
                result = (Decimal)table.Rows.Cast<DataRow>().Sum(x => x.Field<object>(field).toDecimal());

            return result;
        }

        /// <summary>
        /// Calculate total count of a column
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static decimal calcTotal(this DataGridView grid, string field)
        {
            decimal result = 0;

            if (null != grid)
            {
                DataTable table = grid.DataSource as DataTable;

                result = table.calcTotal(field);
            }

            return result;
        }

        /// <summary>
        /// Convert All Gregorian dates to Persian date
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable makePersianDate(this DataTable table, string postfix = "_Persian")
        {
            /*
				1- Find datetime columns
				2- Add _persian columns
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
                            row[col + postfix] = DateTime.Parse(row[col].ToString()).toPersianDate();
            }

            return table;
        }

        /// <summary>
        /// Create Persian date for grid with Gregorian date
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        public static void makePersianDate(this DataTable table, string postfix, params string[] columns)
        {
            if ((null != table) && (columns != null) && (columns.Length > 0))
            {
                // Add columns
                foreach (string col in columns)
                {
                    int index = table.Columns.IndexOf(col);

                    if (index > -1)
                    {
                        table.Columns.Add(col + postfix);
                        table.Columns[col + postfix].SetOrdinal(index + 1);
                    };
                }

                // Convert data
                foreach (DataRow row in table.Rows)
                    foreach (string col in columns)
                        if ((null != row[col]) && (row[col].GetType() != typeof(DBNull)))
                            row[col + postfix] = (DateTime.Parse(row[col].ToString())).toPersianDate();
            }
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

        #region DataGridView
        /// <summary>
        /// Clear datagrid datasource
        /// </summary>
        /// <param name="grid"></param>
        public static void clearDataSource(this DataGridView grid)
        {
            if (null != grid)
            {
                DataTable table = grid.DataSource as DataTable;

                if (table is DataTable)
                    table.Dispose();
                grid.DataSource = null;
            }
        }

        /// <summary>
        /// Fill DataGridView by DataTable that Grid has Columns
        /// </summary>
        /// <param name="table"></param>
        /// <param name="grid"></param>
        public static void fillGridViewHasColumn(this DataGridView grid, DataTable table)
        {
            if (((table != null) && (table.Rows.Count > 0)) && (grid.Columns.Count == table.Columns.Count))
                foreach (DataRow row in table.Rows)
                {
                    object[] dgRow = new object[table.Rows[0].ItemArray.Length];

                    for (int i = 0; i < table.Rows[0].ItemArray.Length; i++)
                        dgRow[i] = row.ItemArray.GetValue(i);

                    grid.Rows.Add(dgRow);
                }
        }

        /// <summary>
        /// Fill DataGridView (grid) by DataTable (dt)
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="table"></param>
        public static void fillGridView(this DataGridView grid, DataTable table)
        {
            if ((table != null) && (table.Rows.Count > 0))
                grid.DataSource = table;
            else
                grid.DataSource = null;
        }

        /// <summary>
        /// Add data row to grid view
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        public static void addRow(this DataGridView grid, DataRowView row)
        {
            if ((row != null) && (row.Row.Table.Columns.Count > 0))
            {
                if (grid.ColumnCount == 0)
                    foreach (DataColumn column in row.Row.Table.Columns)
                        grid.Columns.Add(column.ColumnName, column.ColumnName);

                object[] dgRow = new object[row.Row.ItemArray.Length];

                for (int i = 0; i < row.Row.ItemArray.Length; i++)
                    dgRow[i] = row.Row.ItemArray.GetValue(i);

                grid.Rows.Add(dgRow);
            }
        }

        /// <summary>
        /// Add columns of DataRowView to grid view
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool addColumns(this DataGridView grid, DataRowView row)
        {
            bool result = false;

            if ((row != null) && (row.Row.Table.Columns.Count > 0))
            {
                if (grid.ColumnCount == 0)
                {
                    foreach (DataColumn column in row.Row.Table.Columns)
                        grid.Columns.Add(column.ColumnName, column.ColumnName);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// If first cell of grid view and data table is equal then that row painted green
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="table"></param>
        public static void paintGreenGridView(this DataGridView dataGrid, DataTable table)
        {
            if (dataGrid.DataSource != null && dataGrid.Rows.Count > 0)
                if (table != null && table.Rows.Count > 0)
                    for (int i = 0; i < dataGrid.Rows.Count; i++)
                        for (int j = 0; j < table.Rows.Count; j++)
                        {
                            try
                            {
                                if (int.Parse(dataGrid.Rows[i].Cells[0].Value.ToString()) == int.Parse(table.Rows[j][0].ToString()))
                                    dataGrid.Rows[i].DefaultCellStyle.BackColor = Color.MediumTurquoise;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
        }

        /// <summary>
        /// Set Datagrid Visible State
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="visibleState"></param>
        public static void SetVisible(this DataGridView grid, bool[] visibleState)
        {
            if ((null != grid) && (null != visibleState))
            {
                int mCol = (int)Math.Min(visibleState.Length, grid.ColumnCount);

                for (int i = 0; i < mCol; i++)
                    grid.Columns[i].Visible = visibleState[i];
            }
        }

        /// <summary>
        /// Set Datagrid header
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="header"></param>
        public static void SetHeader(this DataGridView grid, string[] header)
        {
            if ((null != grid) && (null != header))
            {
                int mCol = (int)Math.Min(header.Length, grid.ColumnCount);

                for (int i = 0; i < mCol; i++)
                    grid.Columns[i].HeaderText = header[i];
            }
        }

        /// <summary>
        /// Set DataGrid header to Persian by PersianTranslate function
        /// </summary>
        /// <param name="grid"></param>
        public static void SetPersianHeader(this DataGridView grid)
        {
            if (null != grid)
            {
                int mCol = grid.ColumnCount;

                for (int i = 0; i < mCol; i++)
                    grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.PersianTranslate();
            }
        }
        #endregion
    }
}
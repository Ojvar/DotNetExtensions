using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System
{
	/// <summary>
	/// Extensions
	/// </summary>
    public static partial class Extensions
    {
		#region ComboBox
		/// <summary>
		/// Fill comboBox by DataTable (dt)
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dt"></param>
		/// <param name="value">ValueMember</param>
		/// <param name="display">DisplayMember</param>
		public static void fillComboBox (this ComboBox comboBox, DataTable dt, string value, string display = "")
		{
            if(display == "")
                display = value;
            
			comboBox.DataSource		= dt;
			if ((dt != null) && (dt.Rows.Count > 0))
			{
				comboBox.ValueMember	= value;
				comboBox.DisplayMember	= display;
			}
			if(value == "")
                comboBox.DataSource = null;
		}

		/// <summary>
		/// Fill comboBox by Array of objects (dataList) by key and value
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dataList"></param>
		/// <param name="value">ValueMember</param>
		/// <param name="display">DisplayMember</param>
		public static void fillComboBox (this ComboBox comboBox, Array dataList, string value, string display = "")
		{
            if(display.isNullOrEmptyOrWhiteSpaces ())
                display = value;
            
			comboBox.DataSource		= dataList;
			if (dataList != null)
			{
				comboBox.ValueMember	= value;
				comboBox.DisplayMember	= display;
			}
			if(value == "")
                comboBox.DataSource = null;
		}

		/// <summary>
		/// Fill comboBox by ComboItemModel array
		/// </summary>
		/// <param name="comboBox"></param>
		/// <param name="items"></param>
		public static void fillComboBox (this ComboBox comboBox, Common.Models.ComboItemModel [] items)
		{
            if (null != items)
				comboBox.Items.AddRange (items);
			else if (comboBox.Items.Count > 0)
				comboBox.Items.Clear ();
		}

		/// <summary>
		/// Clear combobox datasource
		/// </summary>
		/// <param name="comboBox"></param>
		public static void clearComboBox (this ComboBox comboBox)
		{
			if (null != comboBox)
			{
				DataTable table	= comboBox.DataSource as DataTable;

				if (table is DataTable)
					table.Dispose ();
				comboBox.DataSource	= null;
			}
		}

		/// <summary>
		/// Unselect item
		/// </summary>
		/// <param name="comboBox"></param>
		public static void unSelect (this ComboBox comboBox)
		{
			if ((null != comboBox) && (comboBox.Items.Count > 0))
			{
				comboBox.SelectedIndex	= -1;
				comboBox.Text			= "";
			}
		}

		/// <summary>
		/// Add items to combo box from DataTable rows by columnName
		/// </summary>
		/// <param name="comboBox"></param>
		/// <param name="dt"></param>
		/// <param name="columnName"></param>
		public static void addItemsComboBox (this ComboBox comboBox, DataTable dt, string columnName)
		{
			if ((dt != null) && (dt.Rows.Count > 0))
			{
				foreach (DataRow dr in dt.Rows)
					comboBox.Items.Add (dr[columnName]);
			}
		}

		/// <summary>
		/// Make Flat Border combobox
		/// </summary>
		/// <param name="box"></param>
		/// <param name="color"></param>
		/// <param name="width"></param>
		public static void addBorder (this Control box, Color color, int width = 1)
		{
			if (box?.Parent != null)
			{
				Panel panel = new Panel ();

				panel.BackColor	= color;
				panel.Size		= box.Size;
				box.Parent.Controls.Add (panel);
				box.Parent.Controls.Remove (box);
				panel.Controls.Add (box);

				panel.Anchor	= box.Anchor;
				box.Anchor		= (AnchorStyles)15;		// ALL

				panel.Location	= box.Location;
				box.Location	= new Point (width, width);
				box.Width		= panel.Width - (width*2);
				box.Height		= panel.Height;
				panel.Height	+= (width*2)+1;
			}
		}
		#endregion
	}
}

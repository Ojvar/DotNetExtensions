using System;
using System.Collections.Generic;
using System.Data;
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
		/// <summary>
		/// Fill Autocomplete
		/// </summary>
		/// <param name="textbox"></param>
		/// <param name="items"></param>
		/// <param name="mode"></param>
		public static void fillAutocomplete (this TextBox textbox, string[] items, AutoCompleteMode mode = AutoCompleteMode.Suggest)
		{
			if ((null != textbox) && (null != items))
			{
				textbox.AutoCompleteCustomSource.Clear ();
				textbox.AutoCompleteCustomSource.AddRange (items);
				textbox.AutoCompleteSource	= AutoCompleteSource.CustomSource;
				textbox.AutoCompleteMode	= mode;
			}
		}

		/// <summary>
		/// Fill Autocomplete
		/// </summary>
		/// <param name="textbox"></param>
		/// <param name="items"></param>
		/// <param name="mode"></param>
		public static void fillAutocomplete (this TextBox textbox, DataTable table, string field, AutoCompleteMode mode = AutoCompleteMode.Suggest)
		{
			if ((null != textbox) && (null != table))
			{
				string[] items = table.Rows.Cast<DataRow> ().Select (x => x.Field<object> (field).ToString ()).ToArray ();

				textbox.fillAutocomplete (items, mode);
			}
		}

		#region TextBox
		/// <summary>
		/// Auto complete text box
		/// </summary>
		/// <param name="textBox"></param>
		/// <param name="items"></param>
		/// <param name="mode"></param>
		public static void fillAutocomplete (this TextBox textBox, Common.Models.ComboItemModel [] items, AutoCompleteMode mode = AutoCompleteMode.SuggestAppend)
		{
			textBox.AutoCompleteSource	= AutoCompleteSource.CustomSource;
			textBox.AutoCompleteCustomSource.Clear ();
			textBox.AutoCompleteMode	= mode;
			if (null != items)
				foreach (Common.Models.ComboItemModel item in items)
					textBox.AutoCompleteCustomSource.Add (item.key);
		}
		#endregion
	}
}

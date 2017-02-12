using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
		#region Methods
		/// <summary>
		/// Fill controls by tag
		/// </summary>
		/// <param name="row"></param>
		/// <param name="control"></param>
		/// <param name="children"></param>
		public static void fillControls (this DataRow row, Control control, bool children = false)
		{
			if ((null != row) && (null != control))
			{
				object[] controls	= control.allControls ();

				foreach (Control c in controls)
					if (0 < c.Tag?.ToString ().Length)
					{
						string	field	= c.Tag.ToString ();

						object value = row[field];

						if (c is TextBox)
							c.Text	= value.ToString ();
						else if (c is MaskedTextBox)
							c.Text	= value.ToString ();
						else if (c is Label)
							c.Text	= value.ToString ();
						else if (c is LinkLabel)
							c.Text	= value.ToString ();
						else if (c is NumericUpDown)
							try
							{
								((NumericUpDown)c).Value	= Convert.ToDecimal (value);
							}
							catch (Exception)
							{
							}
						else if (c is ComboBox)
							try
							{
								((ComboBox)c).SelectedValue	= value;
							}
							catch (Exception)
							{
							}
					}
			}
		}
		#endregion
	}
}

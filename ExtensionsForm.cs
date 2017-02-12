using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Reflection;

namespace System
{
	/// <summary>
	/// Extensions
	/// </summary>
    public static partial class Extensions
	{
		/// <summary>
		/// Property Binder
		/// </summary>
		/// <param name="parent"></param>
		public static void autoBindEvents (this Control parent, string prefix)
		{
			BindingFlags	bf	= BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			
			if (null != parent)
			{
				#region Controls
				MethodInfo[] methodsInfo = parent.GetType ().GetMethods (bf).ToArray ();
				object[] controls = parent.allControls ();

				foreach (object control in controls)
				{
					PropertyInfo	nameProp	= control.GetType ().GetProperty ("Name");
					object			nameValue	= nameProp?.GetValue (control, null);

					if (null == nameValue)
						continue;

					string	preMethod	= string.Format ("{0}{1}_", prefix, nameValue.ToString ());

					// Get Methods
					MethodInfo[] mInfo = methodsInfo.Where (x => x.Name.StartsWith (preMethod)).ToArray ();

					foreach (MethodInfo mi in mInfo)
					{
						string eventName = mi.Name.Remove (0, preMethod.Length);

						EventInfo evInfo = control.GetType ().GetEvent (eventName);
						if (null != evInfo)
						{
							Delegate d = Delegate.CreateDelegate (evInfo.EventHandlerType, parent, mi);

							if (null != d)
								evInfo.AddEventHandler (control, d);
						}
					}
				}
				#endregion
			}
		}

		/// <summary>
		/// Get Controls children
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		public static object[] allControls (this Control control)
		{
			List<object> result	= new List<object> ();
			List<object> queue	= new List<object> ();

			if (null != control)
			{
				queue.Add (control);

				while (0 < queue.Count)
				{
					object item = queue[0];
					queue.RemoveAt (0);

					// Add to output
					result.Add (item);

					// Add menuitem children
					if (item is ToolStripMenuItem)
						foreach (object ctl in ((ToolStripMenuItem)item).DropDownItems)
							queue.Add (ctl);
					// Add Context menu
					else if (item is ToolStripDropDownMenu)
						foreach (object ctl in ((ToolStripDropDownMenu)item).Items)
							queue.Add (ctl);
					// Add menubar items
					else if (item is MenuStrip)
						foreach (object ctl in ((MenuStrip)item).Items)
							queue.Add (ctl);
					// Add toolbar items
					else if (item is ToolStrip)
						foreach (object ctl in ((ToolStrip)item).Items)
							queue.Add (ctl);
					else if (item is Control)
						foreach (object ctl in ((Control)item).Controls)
							queue.Add (ctl);

					#region Add Context Menus
					PropertyInfo pi = item.GetType ().GetProperty ("ContextMenuStrip");
					if (null != pi)
					{
						ContextMenuStrip cm = pi.GetValue (item, null) as ContextMenuStrip;

						if (null != cm)
							queue.Add (cm);
					}

					pi = item.GetType ().GetProperty ("ContextMenu");
					if (null != pi)
					{
						ContextMenu cm = pi.GetValue (item, null) as ContextMenu;

						if (null != cm)
							queue.Add (cm);
					} 
					#endregion
				}
			}
			
			return result.ToArray ();
		}
	}
}

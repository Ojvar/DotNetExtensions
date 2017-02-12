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
		/// <summary>
		/// Get part of an array 
		/// </summary>
		/// <param name="text"></param>
		/// <param name=""></param>
		/// <returns></returns>
		public static byte[] copyPart (this byte[] data, int start, int len)
		{
			byte[]	result	= null;

			if ((null != data) && (start + len <= data.Length))
			{
				result	= new byte[len] ;

				Array.Copy (data, start, result, 0, len);
			}

			return result;
		}

	#region Bit convertor
		/// <summary>
		/// Convert to Int16
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static Int16 toInt16 (this byte[] data)
		{
			Int16 result = 0;

			if (null != data)
			{
				byte[] newData = new byte[data.Length];

				Array.Copy (data, newData, newData.Length);
				//if (BitConverter.IsLittleEndian)
				//	Array.Reverse (newData);

				result	= BitConverter.ToInt16 (newData, 0);
			}

			return result;
		}

		/// <summary>
		/// Convert to Int16
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static Int32 toInt32 (this byte[] data)
		{
			Int32 result = 0;

			if (null != data)
			{
				byte[] newData = new byte[data.Length];

				Array.Copy (data, newData, newData.Length);
				//if (BitConverter.IsLittleEndian)
				//	Array.Reverse (newData);

				result	= BitConverter.ToInt32 (newData, 0);
			}

			return result;
		}
	#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
		/// Object to array
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static byte[] ToByteArray (this object obj)
		{
			byte[]	result = null;

			if (null != obj)
			{
				MemoryStream	ms	= new MemoryStream ();
				BinaryFormatter	bf	= new BinaryFormatter ();

				bf.Serialize (ms, obj);
				
				ms.Seek (0, SeekOrigin.Begin);
				result	= ms.ToArray ();
			}

			return result;
		}

		/// <summary>
		/// Object to array
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static object FromByteArray (this byte[] data)
		{
			object	result = null;

			if (null != data)
			{
				MemoryStream	ms	= new MemoryStream ();
				BinaryFormatter	bf	= new BinaryFormatter ();

				ms.Write (data, 0, data.Length);
				ms.Seek (0, SeekOrigin.Begin);

				result	= bf.Deserialize (ms);
			}

			return result;
		}
	}
}

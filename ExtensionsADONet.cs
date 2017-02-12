using BaseDAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		/// Attach database to sqlserver
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="connectionData"></param>
		/// <returns></returns>
		public static CommandResult attachDB (this string fileName, string dbName, BaseDAL.Model.ConnectionModel connectionData)
		{
			CommandResult result	= null;

			if (File.Exists(fileName) && !dbName.isEmptyOrNullOrWhiteSpaces())
			{
				SqlConnection	connection	= new SqlConnection (connectionData.connectionString);

				result	= BaseDAL.DBaseHelper.executeCommand (BaseDAL.Base.EnumExecuteType.procedureNonQuery, connection, "sp_attach_db", true,
					new KeyValuePair ("@dbname", dbName),
					new KeyValuePair ("@filename1", fileName));
			}
			else
				result	= CommandResult.makeErrorResult ("File not found!");

			return result;
		}
		#endregion
	}
}
	
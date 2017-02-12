using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
		/// Is Number?
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool isNumber (this string text)
		{
			bool result;
			decimal num;

			result  = Decimal.TryParse (text, out num);

			return result;
		}
		
		/// <summary>
		/// Check string is empty or null or white spaces
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool isEmptyOrNullOrWhiteSpaces (this string text)
		{
			bool result	= false;

			// Check input data
			result	= (text == null) || (text.Trim ().Length == 0);

			return result;
		}

		/// <summary>
		/// To Double
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static decimal toDecimal (this object numStr)
		{
			decimal result;
			decimal.TryParse ((numStr??"0").ToString (), out result);

			return result;
		}

		/// <summary>
		/// To Int32
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static int toInt (this object numStr)
		{
			Int32 result;
			Int32.TryParse ((numStr??"0").ToString (), out result);

			return result;
		}
		
		/// <summary>
		/// To Int64
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static long toLong (this object numStr)
		{
			Int64 result;
			Int64.TryParse ((numStr??"0").ToString (), out result);

			return result;
		}

		/// <summary>
		/// To Double
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static double toDouble (this object numStr)
		{
			double result;
			double.TryParse ((numStr??"0").ToString (), out result);

			return result;
		}
		
		/// <summary>
		/// Replace 'ی' and 'ک' from Persian to Arabic ('ي' and 'ك')
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string PersianToArabic (this object text)
		{
			string	result	= "";

			result	= text?.ToString ().Replace ('ی', 'ي').Replace ('ک', 'ك');

			return	result;
		}

        /// <summary>
        /// Replace 'ي' and 'ك' from Arabic to Persian ('ی' and 'ک')
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ArabicToPersian (this string text)
        {
            string result = "";

			if (!text.isNullOrEmptyOrWhiteSpaces ())
				result = text.Replace ('ي', 'ی').Replace ('ك', 'ک');

            return result;
        }

        /// <summary>
        /// Convert text from my English Dictonary to Persian
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PersianTranslate (this string text)
        {
            string result = "";
            text = text.ToLower ();

            for(int i = 0; i < text.Length; i++)
            {
                if (text[i] == '!')
                {
                    result += text[i + 1];
                    i++;
                }
                else if ((48 <= text[i] && text[i] <= 57) || (65<= text[i] && text[i] <= 90) || (97<= text[i] && text[i] <= 122))
                    result += ChangeChar (text[i]);
                else
                    result += text[i];
            }

            return result;
        }

        /// <summary>
        /// Set Character from my Dictionary to Persian Char
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static char ChangeChar (char ch)
        {
            char res = '0';

            switch (ch)
            {
                case '1': res = 'آ'; break;
                case 'a': res = 'ا'; break;
                case 'b': res = 'ب'; break;
                case 'c': res = 'ص'; break;
                case 'd': res = 'د'; break;
                case 'e': res = 'ع'; break;
                case 'f': res = 'ف'; break;
                case 'g': res = 'گ'; break;
                case 'h': res = 'ه'; break;
                case 'i': res = 'ذ'; break;
                case 'j': res = 'ج'; break;
                case 'k': res = 'ک'; break;
                case 'l': res = 'ل'; break;
                case 'm': res = 'م'; break;
                case 'n': res = 'ن'; break;
                case 'o': res = 'ح'; break;
                case 'p': res = 'پ'; break;
                case 'q': res = 'ق'; break;
                case 'r': res = 'ر'; break;
                case 's': res = 'س'; break;
                case 't': res = 'ت'; break;
                case 'u': res = 'غ'; break;
                case 'v': res = 'و'; break;
                case 'w': res = 'ژ'; break;
                case 'x': res = 'خ'; break;
                case 'y': res = 'ی'; break;
                case 'z': res = 'ز'; break;
                case '2': res = 'ض'; break;
                case '3': res = 'ث'; break;
                case '4': res = 'چ'; break;
                case '5': res = 'ظ'; break;
                case '6': res = 'ش'; break;
                case '7': res = 'ط'; break;
                case '8': res = '8'; break;
                case '9': res = '9'; break;
                case '0': res = '0'; break;
            }

            return res;
        }

		/// <summary>
		/// Check string is empty or null or white spaces
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool isNullOrEmptyOrWhiteSpaces (this string text)
		{
			bool result	= false;

			// Check input data
			result	= (text == null) || (text.Trim ().Length == 0);

			return result;
		}

		/// <summary>
		/// Accept float input string
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool isFloat (this string text)
		{
			bool	result	= false;
			Regex	reg		= new Regex (@"^[0-9]*(\.{1}[0-9]+)?$");

			result	= reg.IsMatch (text);

			return	result;
		}

		/// <summary>
		/// Accept digit input string
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool isDigit (this string text)
		{
			bool	result	= false;
			Regex	reg		= new Regex (@"^[0-9]*$");

			result	= reg.IsMatch (text);

			return	result;
		}
	
		/// <summary>
		/// Seprate Digits
		/// </summary>
		/// <param name="number"></param>
		/// <param name="sign"></param>
		/// <param name="groupLen"></param>
		/// <returns></returns>
		public static string separateBySign (this Decimal number, string sign = ",", int groupLen = 3)
		{
			string sNum = number.ToString ();
			string result = "";

			while (sNum.Length > groupLen)
			{
				result = "," + sNum.Substring (sNum.Length - groupLen, groupLen) + result;
				sNum = sNum.Remove (sNum.Length - groupLen, groupLen);
			}
			result = sNum + result;

			return result;
		}

		/// <summary>
		/// Conver string to GUID
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public static Guid toGUID (this string guid)
		{
			Guid result;

			// Create new guid object
			result = new Guid (guid);

			return result;
		}

		/// <summary>
		/// Get N-Digit format
		/// </summary>
		public static string fillByZero (this string digit, int count)
		{
			string res = "";

			res = digit.ToString ();
			for (int i = 0; i<count-digit.ToString ().Length; i++)
				res = "0" + res;

			return res;
		}

		/// <summary>
		/// Apply digit grouping
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string digitGroup (this string text, int step = 3, string separator = ",")
		{
			//return text;

			string	result	= "";

			if (!text.isNullOrEmptyOrWhiteSpaces ())
			{
				while (text.Length > step)
				{
					result  = text.Remove (0, text.Length-step) + separator + result;
					text	= text.Remove (text.Length-step, step);
				}

				result	= text + (result.Length > 0 ? separator : "") + result;
				if (result.EndsWith (separator))
					result	= result.Remove (result.Length-1, 1);
			}

			return result;
		}

		/// <summary>
		/// Get Bytes of a string
		/// </summary>
		/// <param name="text"></param>
		/// <param name=""></param>
		/// <returns></returns>
		public static byte[] getBytes (this string text, System.Text.Encoding encoding)
		{
			byte[]	result	= null;

			if (null != text)
				result	= encoding.GetBytes (text);

			return result;
		}

		/// <summary>
		/// Get String from bytes
		/// </summary>
		/// <param name="text"></param>
		/// <param name=""></param>
		/// <returns></returns>
		public static string getString (this byte[] data, System.Text.Encoding encoding)
		{
			string result	= null;

			if (null != data)
				result	= encoding.GetString (data, 0, data.Length);

			return result;
		}
		#endregion
	}
}

using System;
using System.Text;

namespace Bkav.eGovCloud.Business.Utils
{
	/// <summary>
	/// 
	/// </summary>
	public static class RandomHelper
	{
		/// <summary>
		/// Get Random string
		/// </summary>
		/// <param name="size"></param>
		/// <param name="lowerCase"></param>
		/// <returns></returns>
		public static string RandomString(int size, bool lowerCase)
		{
			var builder = new StringBuilder();
			var random = new Random();
			char ch;
			for (int i = 0; i < size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}
			if (lowerCase)
				return builder.ToString().ToLower();
			return builder.ToString();
		}
	}
}

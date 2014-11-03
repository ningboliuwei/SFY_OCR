#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SFY_OCR.Properties;

#endregion

namespace SFY_OCR
{
	internal class Common
	{

		/// <summary>
		///     读取指定路径的文件内容
		/// </summary>
		/// <param name="filePath">要读取的文件的路径</param>
		/// <returns>文件内容</returns>
		public static string GetFileContent(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
				{
					StreamReader streamReader = new StreamReader(filePath);

					string s = streamReader.ReadToEnd();
					streamReader.Close();

					return s;
				}

				return "";
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
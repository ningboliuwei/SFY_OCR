#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
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


		public static ImageFormat GetImageFormatByExtName(string extName)
		{
			switch (extName.ToUpper())
			{
				case "JPEG":
				case "JPG":
					return ImageFormat.Jpeg;
					break;
				case "TIFF":
				case "TIF":
					return ImageFormat.Tiff;
					break;
				case "PNG":
					return ImageFormat.Png;
					break;
				case "BMP":
					return ImageFormat.Bmp;
					break;
				case "GIF":
					return ImageFormat.Gif;
					break;
				default:
					return null;
					break;

			}
		}
		
		/// <summary>
		/// 判断指定文件是否正在使用中
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static bool IsInUse(string filePath)
		{
			bool inUse = true;

			FileStream fs = null;
			try
			{
				fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);

				inUse = false;
			}
			catch
			{

			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
				}
			}
			return inUse;//true表示正在使用,false没有使用  
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SFY_OCR
{
	internal class Common
	{
		public static void InvokeOcrCommandLine(string tesseractOcrDir, string sourceImagePath, string outputPath,
			string langType)
		{
			using (Process process = new System.Diagnostics.Process())
			{
				process.StartInfo.FileName = tesseractOcrDir + "\\tesseract.exe";
				process.StartInfo.Arguments = sourceImagePath + " " + outputPath + " -l " + langType;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.Start();
				process.WaitForExit();
			}
		}

		/// <summary>
		/// 读取指定路径的文件内容
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
			catch (Exception exception)
			{

				throw new Exception(exception.Message);
			}

		}



	}
}

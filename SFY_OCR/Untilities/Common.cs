﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SFY_OCR
{
	internal class Common
	{
		/// <summary>
		/// 调用Tesseract-OCR 命令行
		/// </summary>
		/// <param name="tesseractOcrDir">tesseract.exe所在文件夹</param>
		/// <param name="sourceImagePath">要识别的图片的路径</param>
		/// <param name="outputPath">输出识别结果路径</param>
		/// <param name="langType">选用的语言（字库）</param>
		public static void InvokeOcrCommandLine(string tesseractOcrDir, string sourceImagePath, string outputPath,
			string langType)
		{
			try
			{
				using (Process process = new Process())
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
			catch (Exception exception)
			{
				throw new Exception(exception.Message);
			}
			
		}

		/// <summary>
		/// 调用ImageMagick图片处理程序
		/// </summary>
		/// <param name="imageMagickDir">convert.exe所在文件夹</param>
		/// <param name="args">转换参数</param>
		public static void InvokeImageMagickConvertCommandLine(string imageMagickDir, string args)
		{
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo.FileName = imageMagickDir + "\\convert.exe";
					process.StartInfo.Arguments = args;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.Start();
					process.WaitForExit();
				}
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message);
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

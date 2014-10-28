#region

using System;

#endregion

namespace SFY_OCR.Untilities
{
	public class FileNamePackage
	{
		public FileNamePackage(string filePath)
		{
			FilePath = filePath;
			Dir = filePath.Substring(0, filePath.LastIndexOf("\\", System.StringComparison.Ordinal));
			FullFileName = filePath.Substring(filePath.LastIndexOf("\\", System.StringComparison.Ordinal));
			MainFileName = FullFileName.Substring(0, FullFileName.LastIndexOf(".", StringComparison.Ordinal));
			ExtFileName = FullFileName.Substring(FullFileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
		}

		//所在文件夹路径
		public string Dir { get; private set; }
		//文件路径
		public string FilePath { get; private set; }
		//文件名
		public string FullFileName { get; private set; }
		//主文件名
		public string MainFileName { get; private set; }
		//扩展名
		public string ExtFileName { get; private set; }
	}
}
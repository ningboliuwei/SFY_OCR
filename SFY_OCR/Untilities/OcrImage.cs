using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFY_OCR.Untilities
{
	public class OcrImage
	{
		private string _filePath = "";
		private string _fileName = "";
		private string _mainFileName = "";
		private string _extFileName = "";

		public OcrImage(string filePath)
		{
			_filePath = filePath;
			_fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
			_mainFileName = _fileName.Substring(0, _fileName.LastIndexOf("."));
			_extFileName = _fileName.Substring(_fileName.LastIndexOf(".") + 1);

		}

		public string FilePath
		{
			get { return _filePath; }
		}

		public string MainFileName
		{
			get { return _mainFileName; }
		}

		public string FileName
		{
			get { return _fileName; }

		}

		public string ExtFileName
		{
			get { return _extFileName; }

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFY_OCR.Properties;

namespace SFY_OCR.Untilities
{
	class TessProcess : CommandLineProcess
	{
		public TessProcess()
			: base()
		{
			_commandPath = Settings.Default.TesseractOcrDir + "\\tesseract.exe";
		}

	}
}

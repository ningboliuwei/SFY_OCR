using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SFY_OCR.Properties;

namespace SFY_OCR.Untilities
{
	public abstract class ImageProcess:CommandLineProcess
	{
		public ImageProcess() : base()
		{
			_commandPath = Settings.Default.ImageMagickDir + "\\convert.exe";
		}
	}
}
